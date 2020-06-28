using System;
using System.Collections.Generic;
using System.Linq;
using URandom = UnityEngine.Random;

namespace BeardPhantom.UCL.Utility
{
    public static class CollectionUtility
    {
        #region Methods

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;

            while (n > 1)
            {
                n--;
                var k = URandom.Range(0, n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            var list = enumerable as IList<T>;

            if (list == null)
            {
                list = enumerable.ToList();
            }

            return list.Count == 0
                ? default
                : list[URandom.Range(0, list.Count)];
        }

        public static T[] Random<T>(this IEnumerable<T> enumerable, int count)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = enumerable.Random();
            }

            return array;
        }

        public static void AddOrSet<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static IEnumerable<T> Append<T>(
            this IEnumerable<T> source,
            T item)
        {
            foreach (var i in source)
            {
                yield return i;
            }

            yield return item;
        }

        public static IEnumerable<T> Prepend<T>(
            this IEnumerable<T> source,
            T item)
        {
            yield return item;

            foreach (var i in source)
            {
                yield return i;
            }
        }

        #endregion
    }
}