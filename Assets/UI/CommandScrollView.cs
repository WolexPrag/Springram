using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CommandScrollView : MonoBehaviour, ICommandViewBus
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<CommandView> _commands = new();
    public CommandView[] Commands => _commands.ToArray();
    private Subject<(CommandView, EventStateType, PointerEventData)> _drag = new();
    public Observable<(CommandView, EventStateType, PointerEventData)> OnDrag => _drag;
    private Subject<(CommandView, PointerEventData)> _drop = new();
    public Observable<(CommandView, PointerEventData)> OnDrop => _drop;
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
    public void OnNextDrag((CommandView, EventStateType, PointerEventData) _)
    {
        _drag?.OnNext(_);
    }

    public void OnNextDrop((CommandView, PointerEventData) _)
    {
        _drop?.OnNext(_);
    }
}