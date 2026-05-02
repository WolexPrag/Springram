using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using R3;

public class VisualIDEView : IDEView
{
    [SerializeField] private BlockPanel _registryPanel;
    [SerializeField] private BlockPanel _inputPanel;

    [SerializeField] private int _lastSibling = -1;
    [SerializeField] private Block _heldItem;
    private CompositeDisposable _disposable = new();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        _registryPanel.Init(transform);
        _inputPanel.Init(transform);
    }
    public void DeInit()
    {
        _disposable?.Dispose();
    }
    private void Return(Block view, BlockPanel scrollView)
    {
        scrollView.Insert(_lastSibling, view);
    }
    public Block CreateNew()
    {
        return Instantiate(_heldItem);

    }

    public override Command[] GetInput()
    {
        return _inputPanel.Blocks.Select(_ => _.Command).ToArray();
    }
}