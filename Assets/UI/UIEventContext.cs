using UnityEngine;
using UnityEngine.EventSystems;
public struct UIEventContext<T>
{
    public T Target { get; private set; }
    public Vector2 Delta { get; private set; }
    public UIEventContext(T target,PointerEventData evenData)
    {
        Target = target;
        Delta = evenData.delta;
    }
}

