using System;
using System.Collections.Generic;

namespace Datastructures
{
    public static class IListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }

        public static void RemoveRange<T>(this IList<T> list, int index, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count cannot be negative");
            }

            if (index < 0)
            {
                throw new ArgumentException("Index cannot be negative");
            }

            if (index + count - 1 >= list.Count)
            {
                throw new ArgumentException("Range is out of bounds of IList");
            }

            for (int i = 0; i < count; ++i)
            {
                list.RemoveAt(index);
            }
        }
    }
}