using System;
using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    /// <summary>
    /// A generic GUI scope that sets and restores a single value
    /// </summary>
    public abstract class GUIScope<T> : IDisposable
    {
        #region Fields

        private readonly T _previousValue;

        #endregion

        #region Constructors

        protected GUIScope(T value)
        {
            _previousValue = GetCurrentValue();
            SetCurrentValue(value);
        }

        #endregion

        #region Methods

        protected abstract T GetCurrentValue();

        protected abstract void SetCurrentValue(T value);

        /// <inheritdoc />
        public void Dispose()
        {
            SetCurrentValue(_previousValue);
        }

        #endregion
    }

    /// <summary>
    /// Scope for GUI.color
    /// </summary>
    public class GUIColorScope : GUIScope<Color>
    {
        #region Constructors

        /// <inheritdoc />
        public GUIColorScope(Color value) : base(value) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override Color GetCurrentValue()
        {
            return GUI.color;
        }

        /// <inheritdoc />
        protected override void SetCurrentValue(Color value)
        {
            GUI.color = value;
        }

        #endregion
    }

    /// <summary>
    /// Scope for GUI.backgroundColor
    /// </summary>
    public class GUIBackgroundColorScope : GUIScope<Color>
    {
        #region Constructors

        /// <inheritdoc />
        public GUIBackgroundColorScope(Color value) : base(value) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override Color GetCurrentValue()
        {
            return GUI.backgroundColor;
        }

        /// <inheritdoc />
        protected override void SetCurrentValue(Color value)
        {
            GUI.backgroundColor = value;
        }

        #endregion
    }

    /// <summary>
    /// Scope for GUI.contentColor
    /// </summary>
    public class GUIContentColorScope : GUIScope<Color>
    {
        #region Constructors

        /// <inheritdoc />
        public GUIContentColorScope(Color value) : base(value) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override Color GetCurrentValue()
        {
            return GUI.contentColor;
        }

        /// <inheritdoc />
        protected override void SetCurrentValue(Color value)
        {
            GUI.contentColor = value;
        }

        #endregion
    }

    /// <summary>
    /// Scope for GUI.enabled
    /// </summary>
    public class GUIEnabledScope : GUIScope<bool>
    {
        #region Constructors

        /// <inheritdoc />
        public GUIEnabledScope(bool value) : base(value) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override bool GetCurrentValue()
        {
            return GUI.enabled;
        }

        /// <inheritdoc />
        protected override void SetCurrentValue(bool value)
        {
            GUI.enabled = value;
        }

        #endregion
    }
}