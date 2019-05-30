using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BeardPhantom.UCL.Editor
{
    public static class UIElementsUtility
    {
        #region Methods

        public static void CreateSerializedPropertyFields(
            SerializedObject serializedObject,
            VisualElement parent,
            params string[] ignoredProperties)
        {
            var propIter = serializedObject.GetIterator();
            propIter.NextVisible(true);
            do
            {
                var scriptField = new PropertyField(propIter);
                var shouldInclude = Array.IndexOf(ignoredProperties, propIter.name) < 0;
                scriptField.SetEnabled(shouldInclude);
                parent.Add(scriptField);
            }
            while (propIter.NextVisible(false));
        }

        #endregion
    }
}