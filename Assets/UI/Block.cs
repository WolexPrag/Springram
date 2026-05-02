using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class Block : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private IUIEventBus<Block> _bus;

    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    public Command Command { get; private set; }
    public void Init(IUIEventBus<Block> bus)
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
        _bus?.BeginDrag(new UIEventContext<Block>(this,eventData));
    }
    public void OnDrag(PointerEventData eventData)
    {
        _bus?.PerformDrag(new UIEventContext<Block>(this, eventData));
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _bus?.EndDrag(new UIEventContext<Block>(this, eventData));
        _canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        _bus?.Drop(new UIEventContext<Block>(this, eventData));
    }
}

