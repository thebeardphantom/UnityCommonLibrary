using System.Collections.Generic;

namespace BeardPhantom.UCL.Pooling
{
    /// <summary>
    /// Pool of Lists
    /// </summary>
    public sealed class ListPool<T> : CollectionPool<T, List<T>, ListPool<T>>
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