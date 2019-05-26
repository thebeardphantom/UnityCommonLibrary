using System.Collections.Generic;

namespace BeardPhantom.UCL.Pooling
{
    /// <summary>
    /// Pool of HashSets
    /// </summary>
    public class HashSetPool<T> : CollectionPool<T, HashSet<T>, HashSetPool<T>>
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