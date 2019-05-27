using System;
using UnityEditor;

namespace BeardPhantom.UCL.Editor
{
    public struct IndentScope : IDisposable
    {
        #region Fields

        private readonly int _indentOffset;

        #endregion

        #region Constructors

        public IndentScope(int indentOffset)
        {
            _indentOffset = indentOffset;
            EditorGUI.indentLevel += _indentOffset;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            EditorGUI.indentLevel -= _indentOffset;
        }

        #endregion
    }
}