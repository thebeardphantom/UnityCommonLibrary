using System.Collections.Generic;
using BeardPhantom.UCL.Attributes;
using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// A component that helps to reference a gameobject indirectly.
    /// </summary>
    public class SubObjectReference : MonoBehaviour
    {
        #region Fields

        private static readonly Dictionary<string, SubObjectReference> _refLookup
            = new Dictionary<string, SubObjectReference>();

        #endregion

        #region Properties

        [field: ReadOnly]
        [field: SerializeField]
        [field: HideInInspector]
        public string Guid { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Find a SubObjectReference by Guid.
        /// </summary>
        public static SubObjectReference Find(string guid)
        {
            _refLookup.TryGetValue(guid, out var value);
            return value;
        }

        private void Awake()
        {
            _refLookup[Guid] = this;
        }

        #endregion
    }
}