
using UnityEngine.EventSystems;

public interface ICommandViewBus
{
    public void OnNextDrag((CommandView, EventStateType, PointerEventData) _);
    public void OnNextDrop((CommandView, PointerEventData) _);
}


public enum EventStateType
{
    Begin,
    Perform,
    End,
}