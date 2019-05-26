using UnityEditor;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomEditor(typeof(SubObjectReference))]
    public class SubObjectReferenceEditor : UnityEditor.Editor
    {
        #region Methods

        private void GenerateGuid()
        {
            serializedObject.Update();
            var guid = serializedObject.FindProperty("_guid");

            if (string.IsNullOrWhiteSpace(guid.stringValue))
            {
                var objIdentifier =
                    serializedObject.FindProperty("m_LocalIdentfierInFile");

                var obj = target as SubObjectReference;
                var prefab = GameObjectEditorUtility.GetPrefab(obj.gameObject);
                var path = AssetDatabase.GetAssetPath(prefab);
                var prefabGuid = AssetDatabase.AssetPathToGUID(path);
                guid.stringValue = $"{prefabGuid}_{objIdentifier.intValue}";
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void OnEnable()
        {
            GenerateGuid();
        }

        #endregion
    }
}