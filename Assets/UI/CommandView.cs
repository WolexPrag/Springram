using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class CommandView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    public Command Command { get; private set; }
    public void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _bus?.OnNextDrag((this, EventStateType.Begin, eventData));
    }
    public void OnDrag(PointerEventData eventData)
    {
        _bus?.OnNextDrag((this, EventStateType.Perform, eventData));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _bus?.OnNextDrag((this, EventStateType.End, eventData));
        _canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        _bus?.OnNextDrop((this, eventData));
    }
}

