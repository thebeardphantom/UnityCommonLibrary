using BeardPhantom.UCL.Attributes;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomPropertyDrawer(typeof(DisplayNameAttribute))]
    public class DisplayNameAttributeDrawer : PropertyDrawer
    {
        #region Methods

        public override float GetPropertyHeight(
            SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(
                property,
                (attribute as DisplayNameAttribute).Label);
        }

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.PropertyField(
                position,
                property,
                (attribute as DisplayNameAttribute).Label,
                true);
        }

        #endregion
    }
}