using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BlockPanel : MonoBehaviour, IUIEventBus<Block>, IUIEventNotifier<Block>
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<Block> _blocks = new();
    public Block[] Blocks => _blocks.ToArray();

    private Subject<UIEventContext<Block>> _onBeginDrag = new();
    private Subject<UIEventContext<Block>> _onPerformDrag = new();
    private Subject<UIEventContext<Block>> _onEndDrag = new();
    private Subject<UIEventContext<Block>> _onDrop = new();

    public Observable<UIEventContext<Block>> OnBeginDrag => _onBeginDrag;
    public Observable<UIEventContext<Block>> OnPerformDrag => _onPerformDrag;
    public Observable<UIEventContext<Block>> OnEndDrag => _onEndDrag;
    public Observable<UIEventContext<Block>> OnDrop => _onDrop;

    private Transform _intire;

    public void Init(Transform intire)
    {
        _intire = intire;
        if (_blocks?.Count > 0)
        {
            for (int i = 0; i < _blocks.Count; i++) 
            {
                _blocks[i].Init(this);
            }
        }
    }
    public void Insert(Block internalBlock, Block block)
    {
        Insert(_blocks.IndexOf(internalBlock), block);
    }
    public void Insert(int id, Block block)
    {
        if (_blocks.Count >= id)
        {
            _blocks.Add(block);
        }
        else
        {
        _blocks.Insert(id, block);
        }
        block.transform.SetParent(_container,false);
        block.transform.SetSiblingIndex(id);
        block.Init(this);
    }
    public void Peek(Block block)
    {
        _blocks.Remove(block);
        block.transform.SetParent(_intire,true);
    }

    public void BeginDrag(UIEventContext<Block> context)
    {
        _onBeginDrag?.OnNext(context);
    }

    public void PerformDrag(UIEventContext<Block> context)
    {
        _onPerformDrag?.OnNext(context);
    }

    public void EndDrag(UIEventContext<Block> context)
    {
        _onEndDrag?.OnNext(context);
    }

    public void Drop(UIEventContext<Block> context)
    {
        _onDrop?.OnNext(context);
    }
}