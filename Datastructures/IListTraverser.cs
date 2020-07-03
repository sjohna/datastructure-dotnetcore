namespace Datastructures
{
    public interface IListTraverser<T>
    {
        bool AtStart { get; }

        bool AtEnd { get; }

        T Element { get; set; }

        int Index { get; }

        void RemoveAt();

        void InsertBefore(T newElement);

        void InsertAfter(T newElement);

        void ToNext();

        void ToPrevious();
    }
}