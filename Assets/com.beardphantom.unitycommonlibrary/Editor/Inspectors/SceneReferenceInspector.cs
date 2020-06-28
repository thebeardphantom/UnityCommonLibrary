using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    /// <summary>
    /// Drawer for <see cref="SceneReference" />
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceInspector : PropertyDrawer
    {
        #region Methods

        public override float GetPropertyHeight(
            SerializedProperty property,
            GUIContent label)
        {
            return UnityEditor.EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            var pathProperty = property.FindPropertyRelative("_rawPath");
            var currentScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathProperty.stringValue);
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                var newScene = (SceneAsset) EditorGUI.ObjectField(
                    position,
                    label,
                    currentScene,
                    typeof(SceneAsset),
                    false);

                if (changeCheck.changed)
                {
                    pathProperty.stringValue = AssetDatabase.GetAssetOrScenePath(newScene);
                }
            }
        }

        #endregion
    }
}