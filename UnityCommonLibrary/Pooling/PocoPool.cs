using System;
using System.Collections.Concurrent;

namespace BeardPhantom.UCL.Pooling
{
    public abstract class BasePocoPool<T1, T2> : IDisposable
        where T1 : new()
        where T2 : BasePocoPool<T1, T2>, new()
    {
        #region Fields

        /// <summary>
        /// Pool of available collections
        /// </summary>
        protected static readonly ConcurrentQueue<T2> Pool = new ConcurrentQueue<T2>();

        /// <summary>
        /// Collection resource
        /// </summary>
        public readonly T1 Object = new T1();

        #endregion

        #region Methods

        /// <summary>
        /// Obtain collection wrapper from pool
        /// </summary>
        public static T2 Obtain()
        {
            if (Pool.Count == 0 || Pool.TryDequeue(out var pool))
            {
                pool = new T2();
            }

            return pool;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            lock (Pool)
            {
                Pool.Enqueue((T2) this);
            }
        }

        /// <summary>
        /// </summary>
        public static implicit operator T1(BasePocoPool<T1, T2> pool)
        {
            return pool.Object;
        }

        #endregion
    }

    public sealed class PocoPool<T1> : BasePocoPool<T1, PocoPool<T1>> where T1 : new() { }
}