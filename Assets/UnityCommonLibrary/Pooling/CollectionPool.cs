using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BeardPhantom.UCL.Pooling
{
    /// <summary>
    /// Base class for any pool of collections of type T1
    /// </summary>
    /// <typeparam name="T1">The collection element type</typeparam>
    /// <typeparam name="T2">The collection type</typeparam>
    /// <typeparam name="T3">The collection pool class type</typeparam>
    public abstract class CollectionPool<T1, T2, T3> : IDisposable, IEnumerable<T1>
        where T2 : IEnumerable<T1>, new()
        where T3 : CollectionPool<T1, T2, T3>, new()
    {
        #region Fields

        /// <summary>
        /// Pool of available collections
        /// </summary>
        protected static readonly ConcurrentQueue<T3> Pool = new ConcurrentQueue<T3>();

        /// <summary>
        /// Collection resource
        /// </summary>
        public readonly T2 Collection = new T2();

        #endregion

        #region Methods

        /// <summary>
        /// Obtain collection wrapper from pool
        /// </summary>
        public static T3 Obtain()
        {
            if (Pool.Count == 0 || Pool.TryDequeue(out var pool))
            {
                pool = new T3();
            }

            return pool;
        }

        /// <summary>
        /// Prepare collection to be returned to pool
        /// </summary>
        protected abstract void Return();

        /// <inheritdoc />
        public void Dispose()
        {
            lock (Pool)
            {
                Return();
                Pool.Enqueue((T3) this);
            }
        }

        /// <inheritdoc />
        public IEnumerator<T1> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// </summary>
        public static implicit operator T2(CollectionPool<T1, T2, T3> pool)
        {
            return pool.Collection;
        }

        #endregion
    }
}