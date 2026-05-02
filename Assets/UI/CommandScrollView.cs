using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CommandScrollView : MonoBehaviour, IUIEventBus<CommandView>, IUIEventNotifier<CommandView>
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<CommandView> _commands = new();
    public CommandView[] Commands => _commands.ToArray();

    private Subject<UIEventContext<CommandView>> _onBeginDrag = new();
    private Subject<UIEventContext<CommandView>> _onPerformDrag = new();
    private Subject<UIEventContext<CommandView>> _onEndDrag = new();
    private Subject<UIEventContext<CommandView>> _onDrop = new();

    public Observable<UIEventContext<CommandView>> OnBeginDrag => _onBeginDrag;
    public Observable<UIEventContext<CommandView>> OnPerformDrag => _onPerformDrag;
    public Observable<UIEventContext<CommandView>> OnEndDrag => _onEndDrag;
    public Observable<UIEventContext<CommandView>> OnDrop => _onDrop;

    private Transform _intire;

    public void Init(Transform intire)
    {
        _intire = intire;
        if (_commands?.Count > 0)
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                _commands[i].Init(this);
            }
        }
    }
    public void InsertCommand(CommandView internalCommand, CommandView command)
    {
        InsertCommand(_commands.IndexOf(internalCommand), command);
    }
    public void InsertCommand(int id, CommandView command)
    {
        if (_commands.Count >= id)
        {
            _commands.Add(command);
        }
        else
        {
        _commands.Insert(id, command);
        }
        command.transform.SetParent(_container,false);
        command.transform.SetSiblingIndex(id);
        command.Init(this);
    }
    public void PeekCommand(CommandView command)
    {
        _commands.Remove(command);
        command.transform.SetParent(_intire,true);
    }

    public void BeginDrag(UIEventContext<CommandView> context)
    {
        _onBeginDrag?.OnNext(context);
    }

    public void PerformDrag(UIEventContext<CommandView> context)
    {
        _onPerformDrag?.OnNext(context);
    }

    public void EndDrag(UIEventContext<CommandView> context)
    {
        _onEndDrag?.OnNext(context);
    }

    public void Drop(UIEventContext<CommandView> context)
    {
        _onDrop?.OnNext(context);
    }
}