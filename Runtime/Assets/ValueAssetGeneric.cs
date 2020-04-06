using BeardPhantom.UCL.Attributes;
using UnityEngine;

namespace BeardPhantom.UCL.Assets
{
    /// <summary>
    /// A value asset that represents a single value.
    /// </summary>
    public abstract class ValueAsset<T> : ValueAsset
    {
        [SerializeField]
        private T _value;

        #region Fields

        #endregion

        #region Properties

        
        public virtual T Value => _value;

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