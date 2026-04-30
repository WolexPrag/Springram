using UnityEngine;
using UnityEngine.EventSystems;
public class CommandView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform _rect;
    public Command Command { get; private set; }
    public void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void Bind(Command command)
    {
        Command = command;
    }
    public void MoveDelta(Vector2 delta)
    {
        if (_rect == null) return;
        _rect.anchoredPosition += delta;
    }

    private ICommandViewBus _bus;
    public void Init(ICommandViewBus bus)
    {
        _bus = bus;
    }
    public void OnDrag(PointerEventData eventData) => _bus?.OnNextDrag((this, EventStateType.Begin, eventData));
    public void OnBeginDrag(PointerEventData eventData) => _bus?.OnNextDrag((this, EventStateType.Perform, eventData));
    public void OnEndDrag(PointerEventData eventData) => _bus?.OnNextDrag((this, EventStateType.End, eventData));
    public void OnDrop(PointerEventData eventData) => _bus?.OnNextDrop((this, eventData));
}

