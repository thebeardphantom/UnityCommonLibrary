using System.Collections.Generic;

namespace BeardPhantom.UCL.Pooling
{
    /// <summary>
    /// Pool of Dictionaries
    /// </summary>
    public sealed class
        DictionaryPool<K, V> : CollectionPool<KeyValuePair<K, V>, Dictionary<K, V>, DictionaryPool<K, V>>
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