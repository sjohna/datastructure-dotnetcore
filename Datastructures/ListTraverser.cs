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

            SetStateProperties();
        }

        public int Index => OnIndex ? m_index : throw new InvalidOperationException("Traverser is not on a valid index of the list.");

        public T Element 
        { 
            get => OnElement ? m_element : throw new InvalidOperationException("Traverser is not on a valid element of the list.");
            set 
            {
                if (!OnIndex) throw new InvalidOperationException("Traverser is not on a valid element of the list.");

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

        public bool OnElement 
        {
            get;
            private set;
        }

        public bool OnIndex
        {
            get;
            private set;
        }

        public void InsertAfter(T newElement)
        {
            if (!OnIndex && !AtStart) m_list.Insert(m_index, newElement);
            else if (AtStart) 
            {
                m_list.Insert(0, newElement);
                m_index = -1;
            }
            else m_list.Insert(m_index+1, newElement); 
        }

        public void InsertBefore(T newElement)
        {
            if (AtStart) 
            {
                m_list.Insert(0, newElement);
                m_index = 1;
            }
            else 
            {
                m_list.Insert(m_index,newElement);
                ++m_index;  
            }          
        }

        public void RemoveAt()
        {
            if (!OnIndex) throw new InvalidOperationException("Traverser is not on a removable element of the list.");

            m_list.RemoveAt(m_index);
            m_removedCurrent = true;
            OnIndex = false;
            if (m_index == 0 && m_list.Count != 0)
            {
                m_index = -1;
            }
        }

        public void ToNext()
        {
            if (OnIndex || AtStart) m_index++;
            SetStateProperties();
        }

        public void ToPrevious()
        {
            if (!AtStart) m_index--;
            SetStateProperties();
        }

        private void SetStateProperties()
        {
            m_removedCurrent = false;
            if (m_list.Count == 0) m_index = 0; // always set traverser index to 0 for an empty list

            if (InBounds) 
            {
                m_element = m_list[m_index];
                OnElement = true;
                OnIndex = true;
            }
            else
            {
                OnElement = false;
                OnIndex = false;
            }            
        }
    }
}