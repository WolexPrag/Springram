using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using R3;

public class VisualIDEView : IDEView
{
    [SerializeField] private BlockPanel _commandsRegistry;
    [SerializeField] private BlockPanel _commandsInput;

    [SerializeField] private int _lastSibling = -1;
    [SerializeField] private CommandView _heldItem;
    private CompositeDisposable _disposable = new();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        _commandsRegistry.Init(transform);
        _commandsInput.Init(transform);
    }
    public void DeInit()
    {
        _disposable?.Dispose();
    }
    private void Return(CommandView view, BlockPanel scrollView)
    {
        scrollView.InsertCommand(_lastSibling, view);
    }
    public CommandView CreateNew()
    {
        return Instantiate(_heldItem);

    }

    public override Command[] GetInput()
    {
        return _commandsInput.Commands.Select(_ => _.Command).ToArray();
    }
}