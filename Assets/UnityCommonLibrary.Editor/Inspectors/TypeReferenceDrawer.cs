using System;
using System.Linq;
using System.Reflection;
using BeardPhantom.UCL.Attributes;
using BeardPhantom.UCL.Utility;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomPropertyDrawer(typeof(TypeReference))]
    public class TypeReferenceDrawer : PropertyDrawer
    {
        #region Fields

        private Type[] _types;

        private string[] _fullyQualifiedNames;

        private GUIContent[] _names;

        private bool _setup;

        #endregion

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
            if (!_setup)
            {
                Setup(property);
            }

            var subprop = property.FindPropertyRelative("_typeString");

            var index = Array.IndexOf(
                _fullyQualifiedNames,
                subprop.stringValue);

            index = Mathf.Max(0, index);
            index = EditorGUI.Popup(position, label, index, _names);
            subprop.stringValue = _types[index].AssemblyQualifiedName;
        }

        private void Setup(SerializedProperty property)
        {
            _setup = true;
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();

            var field = targetObjectClassType.GetField(
                property.propertyPath,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var attributes = field.GetCustomAttributes(
                typeof(TypeReferenceFilterAttribute),
                true);

            if (attributes == null || attributes.Length == 0)
            {
                return;
            }

            var filter = (attributes[0] as TypeReferenceFilterAttribute).Type;

            _types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.InheritsFrom(filter))
                .OrderBy(t => t.Name)
                .ToArray();

            _fullyQualifiedNames =
                _types.Select(t => t.AssemblyQualifiedName).ToArray();

            _names = _types.Select(t => new GUIContent(t.Name)).ToArray();
        }

        #endregion
    }
}