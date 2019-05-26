using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Null Object Pattern implementation of Unity's log handler.
    /// </summary>
    public class NullLogHandler : ILogHandler
    {
        #region Methods

        /// <inheritdoc />
        public void LogException(Exception exception, Object context) { }

        /// <inheritdoc />
        public void LogFormat(
            LogType logType,
            Object context,
            string format,
            params object[] args) { }

        #endregion
    }
}