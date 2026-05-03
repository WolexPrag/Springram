public interface ITransition<T>
{
    public void Place(object source,T item);
    public T Take(object source);
    public void Abort(object source);
}
