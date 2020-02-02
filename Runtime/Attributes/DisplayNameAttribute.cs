using System;
using UnityEngine;

namespace BeardPhantom.UCL.Attributes
{
    /// <summary>
    /// Attribute for specifying the display name of a serialized field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DisplayNameAttribute : PropertyAttribute
    {
        #region Properties

        /// <summary>
        /// The label to use.
        /// </summary>
        public GUIContent Label { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Makes this field display with a different name in the Inspector.
        /// </summary>
        public DisplayNameAttribute(string name)
        {
            Label = new GUIContent(name);
        }

        #endregion
    }
}