using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        [MenuItem("Assets/Find All References")]
        public static void GetReferences(MenuCommand cmd)
        {
            var refs = GetReferences(cmd.context);
            var str = string.Join("\n", refs);
            Debug.Log(str);
        }

        public static string[] GetReferences(Object asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            var guid = AssetDatabase.AssetPathToGUID(path);
            return GetReferences(guid);
        }

        public static string[] GetReferences(string guid)
        {
            try
            {
                var types = new[]
                {
                    "t:Prefab",
                    "t:ScriptableObject",
                    "t:Material"
                };
                var filter = string.Join(" ", types);
                var allAssetGuids = AssetDatabase.FindAssets(filter);
                var allAssetPaths = allAssetGuids
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .ToArray();
                var path = Application.dataPath;
                path = path.Substring(0, path.Length - 6);
                var assets = new ConcurrentBag<string>();
                var result = Parallel.ForEach(
                    allAssetPaths,
                    assetPath =>
                    {
                        var fullPath = path + assetPath;
                        var text = File.ReadAllText(fullPath);
                        if (text.Contains(guid))
                        {
                            assets.Add(assetPath);
                        }
                    });

                while (!result.IsCompleted)
                {
                    UnityEditor.EditorUtility.DisplayProgressBar(
                        "Finding Assets",
                        "...",
                        0f);
                }

                return assets.ToArray();
            }
            finally
            {
                UnityEditor.EditorUtility.ClearProgressBar();
            }
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