
using UnityEngine.EventSystems;

public interface IUIEventBus<T> 
{
    public void BeginDrag(UIEventContext<T> context);
    public void PerformDrag(UIEventContext<T> context);
    public void EndDrag(UIEventContext<T> context);
    public void Drop(UIEventContext<T> context);
}

