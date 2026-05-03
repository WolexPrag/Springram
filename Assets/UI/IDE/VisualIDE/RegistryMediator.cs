using R3;
using System;
using UnityEngine;
class RegistryMediator : BlockMediator
{
    public RegistryMediator(IUIEventNotifier<Block> notifier, ITransition<Block> transition, IBlockContainer container) : base(notifier, transition, container)
    {
    }

    protected override void BeginDrag(UIEventContext<Block> context)
    {
        int sibling = context.Target.transform.GetSiblingIndex();
        _container.Insert(sibling,GameObject.Instantiate(context.Target));
        base.BeginDrag(context);
    }
    protected override void PerformDrag(UIEventContext<Block> context)
    {
        base.PerformDrag(context);
    }
    protected override void EndDrag(UIEventContext<Block> context)
    {
        GameObject.Destroy(context.Target.gameObject);
    }
    protected override void Drop(UIEventContext<Block> context)
    {
        GameObject.Destroy(_transition.Take(this).gameObject);
    }
}

