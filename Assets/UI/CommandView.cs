using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class CommandView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private IUIEventBus<CommandView> _bus;

    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    public Command Command { get; private set; }
    public void Init(IUIEventBus<CommandView> bus)
    {
        _bus = bus;
    }
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _bus?.BeginDrag(new UIEventContext<CommandView>(this,eventData));
    }
    public void OnDrag(PointerEventData eventData)
    {
        _bus?.PerformDrag(new UIEventContext<CommandView>(this, eventData));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _bus?.EndDrag(new UIEventContext<CommandView>(this, eventData));
        _canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        _bus?.Drop(new UIEventContext<CommandView>(this, eventData));
    }
}

