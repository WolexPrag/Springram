using R3;

public interface IUIEventNotifier<T>
{
    public Observable<UIEventContext<T>> OnBeginDrag { get; }
    public Observable<UIEventContext<T>> OnPerformDrag { get; }
    public Observable<UIEventContext<T>> OnEndDrag { get; }
    public Observable<UIEventContext<T>> OnDrop { get; }
}

