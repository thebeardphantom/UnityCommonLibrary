using System;
using System.Reflection;
using BeardPhantom.UCL.Attributes;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomPropertyDrawer(typeof(EnumMaskAttribute))]
    public class EnumMaskAttributeDrawer : PropertyDrawer
    {
        #region Methods

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            var targetEnum =
                PropertyDrawerUtils.GetBaseProperty<Enum>(property);

            EditorGUI.BeginProperty(position, label, property);
            var enumNew = EditorGUI.EnumFlagsField(position, label, targetEnum);

            property.intValue = (int) Convert.ChangeType(
                enumNew,
                targetEnum.GetType());

            EditorGUI.EndProperty();
        }

        #endregion
    }

    public static class PropertyDrawerUtils
    {
        #region Methods

        public static T GetBaseProperty<T>(SerializedProperty prop)
        {
            // Separate the steps it takes to get to this property
            var separatedPaths = prop.propertyPath.Split('.');

            // Go down to the root of this serialized property
            object reflectionTarget = prop.serializedObject.targetObject;

            // Walk down the path to get the target object
            foreach (var path in separatedPaths)
            {
                var type = reflectionTarget.GetType();
                var fieldInfo = ReflectionUtility.GetFirstFieldInTypeChain(
                    path,
                    type,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo == null)
                {
                    UCLCore.Logger.LogError("", $"Field '{path}' of type '{type.Name}' missing");
                    return default;
                }

                reflectionTarget = fieldInfo.GetValue(reflectionTarget);
            }

            return (T) reflectionTarget;
        }

        #endregion
    }
}