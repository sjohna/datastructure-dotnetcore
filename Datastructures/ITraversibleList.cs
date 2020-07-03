namespace Datastructures
{
    public interface ITraversibleList<T>
    {
        IListTraverser<T> Traverser();

        IListTraverser<T> StartTraverser();

        IListTraverser<T> EndTraverser();

        IListTraverser<T> FirstElementTraverser();

        IListTraverser<T> LastElementTraverser();
    }
}