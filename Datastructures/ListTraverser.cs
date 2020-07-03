using System;
using System.Collections.Generic;

namespace Datastructures
{
    /**
        Provides a generic implementation of traversal for any IList implementation.
    */
    public class ListTraverser<T> : IListTraverser<T>
    {
        private IList<T> m_list;

        private int m_index;

        private bool m_removedCurrent;

        private T m_element;

        private ListTraverser(IList<T> list, int index)
        {
            m_list = list;
            m_index = index;
            m_removedCurrent = false;

            if (InBounds) m_element = m_list[m_index];
        }

        public int Index => InBounds && !m_removedCurrent ? m_index : throw new InvalidOperationException("Traverser is not on a valid index of the list.");

        public T Element 
        { 
            get => InBounds ? m_element : throw new InvalidOperationException("Traverser is not on a valid element of the list.");
            set 
            {
                if (m_removedCurrent || !InBounds) throw new InvalidOperationException("Traverser is not on a valid element of the list.");

                m_list[m_index] = value;
                m_element = m_list[m_index];
            }
        }

        public static ListTraverser<T> TraverserAtIndex(IList<T> list, int index)
        {
            return new ListTraverser<T>(list, index);
        }

        private bool InBounds => !AtStart && !AtEnd;

        public bool AtEnd => m_list.Count == 0 || m_index == m_list.Count;

        public bool AtStart => m_list.Count == 0 || m_index == -1;

        public void InsertAfter(T newElement)
        {
            if (!InBounds) throw new InvalidOperationException("Index after traverser index is not valid for insertion.");

            if (m_removedCurrent) m_list.Insert(m_index, newElement);
            else m_list.Insert(m_index+1, newElement); 
        }

        public void InsertBefore(T newElement)
        {
            if (!InBounds) throw new InvalidOperationException("Index before traverser index is not valid for insertion.");

            m_list.Insert(m_index,newElement);
            ++m_index;               
        }

        public void RemoveAt()
        {
            if (m_removedCurrent) throw new InvalidOperationException("Element already removed.");
            if (!InBounds) throw new InvalidOperationException("Traverser is not on a valid element of the list.");

            m_list.RemoveAt(m_index);
            m_removedCurrent = true;       
        }

        public void ToNext()
        {
            if (AtEnd) throw new InvalidOperationException("Traverser is at end of list.");

            if (!m_removedCurrent) m_index++;
            m_removedCurrent = false;
            if (InBounds) m_element = m_list[m_index];
        }

        public void ToPrevious()
        {
            if (AtStart) throw new InvalidOperationException("Traverser is at start of list.");

            m_index--;
            m_removedCurrent = false;
            if (InBounds) m_element = m_list[m_index];
        }
    }
}