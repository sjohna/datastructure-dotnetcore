using System;
using System.Collections;
using System.Collections.Generic;

namespace Datastructures
{
    public class TraversibleList<List, T> : ITraversibleList<T>, IList<T> where List : IList<T>, new()
    {
        public T this[int index] { get => m_list[index]; set => m_list[index] = value; }

        public int Count => m_list.Count;

        public bool IsReadOnly => m_list.IsReadOnly;

        private List m_list;

        public TraversibleList()
        {
            m_list = new List();
        }

        public void Add(T item) => m_list.Add(item);

        public void Clear() => m_list.Clear();

        public bool Contains(T item) => m_list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => m_list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => m_list.GetEnumerator();

        public int IndexOf(T item) => m_list.IndexOf(item);

        public void Insert(int index, T item) => m_list.Insert(index, item);

        public bool Remove(T item) => m_list.Remove(item);

        public void RemoveAt(int index) => m_list.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => (m_list as IEnumerable).GetEnumerator();

        public IListTraverser<T> LastElementTraverser()
        {
            if (m_list.Count != 0)
                return ListTraverser<T>.TraverserAtIndex(m_list, m_list.Count-1);
            else
                throw new InvalidOperationException("LastElementTraverser not valid on empty list.");
        }

        public IListTraverser<T> StartTraverser()
        {
            return ListTraverser<T>.TraverserAtIndex(m_list, -1);
        }

        public IListTraverser<T> Traverser()
        {
            return ListTraverser<T>.TraverserAtIndex(m_list, 0);
        }

        public IListTraverser<T> EndTraverser()
        {
            return ListTraverser<T>.TraverserAtIndex(m_list, m_list.Count);
        }

        public IListTraverser<T> FirstElementTraverser()
        {
            if (m_list.Count != 0)
                return ListTraverser<T>.TraverserAtIndex(m_list, 0);
            else
                throw new InvalidOperationException("FirstElementTraverser not valid on empty list.");
        }
    }
}