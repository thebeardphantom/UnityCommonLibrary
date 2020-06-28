using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public static class GameObjectEditorUtility
    {
        #region Methods

        public static GameObject GetPrefab(GameObject obj)
        {
            if (PrefabUtility.IsPartOfPrefabAsset(obj))
            {
                return obj.transform.root.gameObject;
            }

            if (PrefabUtility.IsPartOfPrefabInstance(obj))
            {
                return PrefabUtility.GetOutermostPrefabInstanceRoot(obj);
            }

            return null;
        }

        #endregion
    }
}