using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CommandScrollView : MonoBehaviour, ICommandViewBus
{
    [SerializeField] private Transform _container;
    private List<CommandView> _commands;
    public CommandView[] Commands => _commands.ToArray();
    private Subject<(CommandView, EventStateType, PointerEventData)> _drag;
    public Observable<(CommandView, EventStateType, PointerEventData)> OnDrag => _drag;
    private Subject<(CommandView, PointerEventData)> _drop;
    public Observable<(CommandView, PointerEventData)> OnDrop => _drop;

    public void InsertCommand(CommandView internalCommand, CommandView command)
    {
        InsertCommand(_commands.IndexOf(internalCommand), command);
    }
    public void InsertCommand(int id, CommandView command)
    {
        _commands.Insert(id, command);
        command.transform.SetParent(_container);
        command.transform.SetSiblingIndex(id);
        command.Init(this);
    }
    public void PeekCommand(CommandView command)
    {
        _commands.Remove(command);
        command.transform.SetParent(null);

    }
    public void OnNextDrag((CommandView, EventStateType, PointerEventData) _)
    {
        _drag?.OnNext(_);
    }

    public void OnNextDrop((CommandView, PointerEventData) _)
    {
        _drop?.OnNext(_);
    }
}