

using System;
using System.Collections;
using System.Collections.Generic;

namespace Datastructures
{
    public class Vector<T> : ICollection<T>, IList<T>
    {
        private T[] m_items;

        public int Capacity => m_items.Length;

        public int Count 
        {
            get;
            private set;
        }

        public bool IsReadOnly => false;

        public Vector() : this(10)
        {
            
        }

        public Vector(int initialCapacity)
        {
            Count = 0;
            m_items = new T[initialCapacity];
        }

        public void Add(T item)
        {
            GrowBackingArrayIfNeeded();

            m_items[Count] = item;
            ++Count;
        }

        public T this[int index]
        {
            get
            {
                RangeCheckIndexer(index);
                return m_items[index];
            }

            set
            {
                RangeCheckIndexer(index);
                m_items[index] = value;
            }
        }

        private void RangeCheckIndexer(int index)
        {
            if (index < 0 || index >= Count) 
            {
                throw new ArgumentOutOfRangeException($"Index {index} out of range of vector (Count = {Count})");
            }
        }

        private void RangeCheckInsert(int index)
        {
            if (index < 0 || index > Count) 
            {
                throw new ArgumentOutOfRangeException($"Index {index} out of range of vector (Count = {Count})");
            }
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
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
            int itemIndex = IndexOf(item);

            if(itemIndex != -1)
            {
                RemoveAt(itemIndex);
                return true;
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

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(m_items[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            RangeCheckInsert(index);
            GrowBackingArrayIfNeeded();

            for (int shiftIndex = Count - 1; shiftIndex >= index; --shiftIndex)
            {
                m_items[shiftIndex+1] = m_items[shiftIndex];
            }

            m_items[index] = item;

            ++Count;
        }

        private void GrowBackingArrayIfNeeded()
        {
            if (Count == m_items.Length)
            {
                var newItemsArray = new T[m_items.Length * 2];
                m_items.CopyTo(newItemsArray, 0);
                m_items = newItemsArray;
            }           
        }

        public void RemoveAt(int index)
        {
            for (int indexForShift = index+1; indexForShift < Count; ++indexForShift)
            {
                m_items[indexForShift-1] = m_items[indexForShift];
            }

            --Count;            
        }
    }
}