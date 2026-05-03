using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using R3;

public class VisualIDEView : IDEView , ITransition<Block>
{
    [SerializeField] private Transform _canvas;
    [SerializeField] private BlockPanel _registryPanel;
    [SerializeField] private BlockPanel _inputPanel;
    private BlockMediator _registryMediator;
    private BlockMediator _inputMediator;

    [SerializeField] private int _lastSibling = -1;
    [SerializeField] private Block _holdItem;
    private CompositeDisposable _disposable = new();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        _registryPanel.Init();
        _inputPanel.Init();
        _inputMediator = new BlockMediator(_inputPanel, this, _inputPanel);
        _registryMediator = new RegistryMediator(_registryPanel,this,_registryPanel);
    }
    public void DeInit()
    {
        _registryMediator.DeInit();
        _inputMediator.DeInit();
        _disposable?.Dispose();
    }
    public void Place(object source, Block item)
    {
        _holdItem = item;
        _lastSibling = _holdItem.transform.GetSiblingIndex();
        _holdItem.transform.SetParent(_canvas);
    }

    public Block Take(object source)
    {
        _lastSibling = -1;
        Block ret = _holdItem;
        _holdItem = null;
        return ret;
    }
    public void Abort(object source)
    {
        if (_holdItem==null) return; 
        _holdItem.transform.SetSiblingIndex(_lastSibling);
        if (source.GetType() == typeof(BlockMediator)) ((BlockMediator)source).Abort(_holdItem,_lastSibling);
        _holdItem = null;
    }

    public override Command[] GetInput()
    {
        return _inputPanel.Blocks.Select(_ => _.Command).ToArray();
    }
}