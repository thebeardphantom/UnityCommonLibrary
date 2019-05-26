using System;
using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class TextAssetUtility
    {
        #region Fields

        private static readonly string[] _newline =
        {
            Environment.NewLine
        };

        #endregion

        #region Methods

        public static string[] GetLines(this TextAsset asset)
        {
            return asset.text.Split(
                _newline,
                StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion
    }
}