

using System;
using System.Collections;
using System.Collections.Generic;

namespace Datastructures
{
    public class Vector<T> : ICollection<T>
    {
        private T[] m_items;

        public int Count 
        {
            get;
            private set;
        }

        public bool IsReadOnly => false;

        public Vector()
        {
            Count = 0;
            m_items = new T[10];
        }

        public void Add(T item)
        {
            m_items[Count] = item;
            ++Count;
        }

        public T this[int index]
        {
            get
            {
                RangeCheck(index);
                return m_items[index];
            }

            set
            {
                RangeCheck(index);
                m_items[index] = value;
            }
        }

        private void RangeCheck(int index)
        {
            if (index < 0 || index >= Count) 
            {
                throw new IndexOutOfRangeException($"Index {index} out of range of vector (Count = {Count})");
            }
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(m_items[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; ++i)
            {
                array[arrayIndex + i] = m_items[i];
            }
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(m_items[i], item))
                {
                    for (int indexForShift = i+1; indexForShift < Count; ++indexForShift)
                    {
                        m_items[indexForShift-1] = m_items[indexForShift];
                    }

                    --Count;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return m_items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}