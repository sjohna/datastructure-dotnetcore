

using System;

namespace Datastructures
{
    public class Vector<T>
    {
        private T[] m_items;

        public int Count 
        {
            get;
            private set;
        }

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
    }
}