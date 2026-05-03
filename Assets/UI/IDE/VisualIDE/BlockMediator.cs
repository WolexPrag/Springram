using R3;
public class BlockMediator
{
    protected IUIEventNotifier<Block> _notifier;
    protected ITransition<Block> _transition;
    protected IBlockContainer _container;
    private CompositeDisposable _disposable = new();
    public BlockMediator(IUIEventNotifier<Block> notifier, ITransition<Block> transition, IBlockContainer container)
    {
        _notifier = notifier;
        _transition = transition;
        _container = container;
        notifier.OnBeginDrag.Subscribe(BeginDrag).AddTo(_disposable);
        notifier.OnPerformDrag.Subscribe(PerformDrag).AddTo(_disposable);
        notifier.OnEndDrag.Subscribe(EndDrag).AddTo(_disposable);
        notifier.OnDrop.Subscribe(Drop).AddTo(_disposable);
    }
    public virtual void DeInit()
    {
        _disposable?.Dispose();
    }
    protected virtual void BeginDrag(UIEventContext<Block> context)
    {
        context.Target.SetInteractable(false);
        int id = _container.GetIndex(context.Target);
        _container.Remove(id);
        _transition.Place(this, context.Target);
    }
    protected virtual void PerformDrag(UIEventContext<Block> context)
    {
        context.Target.MoveDelta(context.Delta);
    }
    protected virtual void EndDrag(UIEventContext<Block> context)
    {
        context.Target.SetInteractable(true);
        _transition.Abort(this);
    }
    protected virtual void Drop(UIEventContext<Block> context)
    {
        int id = _container.GetIndex(context.Target);
        Block taked = _transition.Take(this);
        _container.Insert(id, taked);
    }
    public void Abort(Block block, int sibling)
    {
        _container.Insert(sibling, block);
    }
}