using System.Collections.Generic;
using UnityEditor;

namespace BeardPhantom.UCL.Editor
{
    public class EditorGUIUtility
    {
        #region Fields

        private static readonly Dictionary<string, bool> _foldouts =
            new Dictionary<string, bool>();

        #endregion

        #region Methods

        public static bool Foldout(string key, string display)
        {
            if (!_foldouts.ContainsKey(key))
            {
                _foldouts.Add(key, true);
            }

            _foldouts[key] = EditorGUILayout.Foldout(_foldouts[key], display);

            return _foldouts[key];
        }

        #endregion
    }
}