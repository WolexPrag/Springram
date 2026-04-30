using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using R3;

public class VisualIDEView : IDEView
{
    [SerializeField] private CommandScrollView _commandsRegistry;
    [SerializeField] private CommandScrollView _commandsInput;

    [SerializeField] private int _lastSibling = -1;
    [SerializeField] private CommandView _heldItem;
    private CompositeDisposable _disposable;
    public void Init()
    {
        _commandsRegistry.OnDrag.Subscribe(_ => OnDrag(_, _commandsRegistry)).AddTo(_disposable);
        _commandsInput.OnDrag.Subscribe(_ => OnDrag(_, _commandsInput)).AddTo(_disposable);
        _commandsInput.OnDrop.Subscribe(_ => OnDrop(_, _commandsInput)).AddTo(_disposable);
    }
    public void DeInit()
    {
        _disposable?.Dispose();
    }
    private void OnDrag((CommandView view, EventStateType state, PointerEventData eventData) interactable, CommandScrollView scrollView)
    {
        switch (interactable.state)
        {
            case EventStateType.Begin:
                _lastSibling = interactable.view.transform.GetSiblingIndex();
                scrollView.PeekCommand(interactable.view);
                _heldItem = interactable.view;
                break;
            case EventStateType.Perform:
                interactable.view.MoveDelta(interactable.eventData.delta);
                break;
            case EventStateType.End:
                Return(interactable.view, scrollView);
                _lastSibling = -1;
                break;
        }

    }
    private void Return(CommandView view, CommandScrollView scrollView)
    {
        scrollView.InsertCommand(_lastSibling, view);
    }
    private void OnDrop((CommandView view, PointerEventData eventData) interactable, CommandScrollView scrollView)
    {
        _commandsInput.InsertCommand(interactable.view,CreateNew());
        Return(interactable.view, scrollView);
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