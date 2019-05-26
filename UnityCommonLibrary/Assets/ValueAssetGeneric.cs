using BeardPhantom.UCL.Attributes;
using UnityEngine;

namespace BeardPhantom.UCL.Assets
{
    /// <summary>
    /// A value asset that represents a single value.
    /// </summary>
    [CustomAssetCreateMenu]
    public abstract class ValueAsset<T> : ValueAsset
    {
        #region Fields

        #endregion

        #region Properties

        [field: SerializeField]
        public virtual T Value { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Implicitly converts to the asset's value.
        /// </summary>
        public static implicit operator T(ValueAsset<T> asset)
        {
            return asset.Value;
        }

        #endregion
    }
}