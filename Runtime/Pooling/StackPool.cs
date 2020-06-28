using System.Collections.Generic;

namespace BeardPhantom.UCL.Pooling
{
    /// <summary>
    /// Pool of Stacks
    /// </summary>
    public sealed class StackPool<T> : CollectionPool<T, Stack<T>, StackPool<T>>
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