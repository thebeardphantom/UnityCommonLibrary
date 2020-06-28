using BeardPhantom.UCL.Attributes;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeInspector : PropertyDrawer
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
            property.stringValue = EditorGUI.TagField(
                position,
                label,
                property.stringValue);
        }

        #endregion
    }
}