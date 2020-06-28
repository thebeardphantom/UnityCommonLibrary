using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public static class EditorGUILayoutControls
    {
        #region Methods

        public static void HorizontalLine()
        {
            EditorGUILayout.LabelField(
                GUIContent.none,
                GUI.skin.horizontalSlider);
        }

        #endregion
    }
}