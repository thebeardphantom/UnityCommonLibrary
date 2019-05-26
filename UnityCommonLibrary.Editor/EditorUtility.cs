using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public static class EditorUtility
    {
        #region Methods

        public static SceneView FindSceneView()
        {
            if (SceneView.currentDrawingSceneView != null)
            {
                return SceneView.currentDrawingSceneView;
            }

            if (SceneView.lastActiveSceneView != null)
            {
                return SceneView.lastActiveSceneView;
            }

            if (SceneView.sceneViews != null)
            {
                foreach (SceneView sceneView in SceneView.sceneViews)
                {
                    if (sceneView != null)
                    {
                        return sceneView;
                    }
                }
            }

            return null;
        }

        public static T[] FindAssets<T>(string search = "") where T : Object
        {
            return AssetDatabase.FindAssets($"{search} t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
        }

        public static string GetSelectedFolder()
        {
            if (Selection.activeObject == null)
            {
                return string.Empty;
            }

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(Path.GetExtension(path)))
            {
                return path;
            }

            return Path.GetDirectoryName(path);
        }

        #endregion
    }
}