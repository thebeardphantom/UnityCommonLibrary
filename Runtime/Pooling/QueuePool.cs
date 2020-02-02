using System.Collections.Generic;

namespace BeardPhantom.UCL.Pooling
{
    /// <summary>
    /// Pool of Queues
    /// </summary>
    public sealed class QueuePool<T> : CollectionPool<T, Queue<T>, QueuePool<T>>
    {
        #region Methods

        /// <inheritdoc />
        protected override void Return()
        {
            Collection.Clear();
        }

        #endregion
    }
}