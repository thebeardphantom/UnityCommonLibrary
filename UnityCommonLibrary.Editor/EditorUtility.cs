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
        public static void GetReferences()
        {
            var refs = GetReferences(Selection.activeObject);
            if(refs.Length > 0)
            {
                var str = string.Join("\n", refs);
                Debug.Log(str);
            }
        }

        [MenuItem("Assets/Safe Delete")]
        public static void SafeDeleteAsset()
        {
            var asset = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            var refs = GetReferences(AssetDatabase.AssetPathToGUID(path));
            if (refs.Length == 0)
            {
                var wantsDelete = UnityEditor.EditorUtility.DisplayDialog(
                    "Safe Delete",
                    "No references found, delete?",
                    "Delete",
                    "Cancel");
                if (wantsDelete)
                {
                    AssetDatabase.DeleteAsset(path);
                }
            }
            else
            {
                var str = string.Join("\n", refs);
                Debug.Log(str);
            }
        }

        public static string[] GetReferences(Object asset)
        {
            if (asset == null)
            {
                return new string[0];
            }

            var path = AssetDatabase.GetAssetPath(asset);
            var guid = AssetDatabase.AssetPathToGUID(path);
            return GetReferences(guid);
        }

        public static string[] GetReferences(string searchGuid)
        {
            if (string.IsNullOrWhiteSpace(searchGuid))
            {
                return new string[0];
            }

            try
            {
                var contentSearchableExts = new[]
                {
                    ".unity",
                    ".prefab",
                    ".mat",
                    ".asset"
                };

                var searchPath = AssetDatabase.GUIDToAssetPath(searchGuid);
                var allAssetPaths = AssetDatabase
                    .GetAllAssetPaths()
                    .Except(new[] { searchPath });

                var projectPath = Application.dataPath;
                projectPath = projectPath.Substring(0, projectPath.Length - 6);

                var referencers = new ConcurrentBag<string>();
                var result = Parallel.ForEach(
                    allAssetPaths,
                    assetPath =>
                    {
                        var didAdd = false;
                        var fullPath = projectPath + assetPath;
                        var metaPath = fullPath + ".meta";
                        var assetExt = Path.GetExtension(fullPath);
                        if (contentSearchableExts.Contains(assetExt))
                        {
                            var assetContents = File.ReadAllText(fullPath);
                            if (assetContents.Contains(searchGuid))
                            {
                                referencers.Add(assetPath);
                                didAdd = true;
                            }
                        }

                        if (!didAdd && File.Exists(metaPath))
                        {
                            var metaContents = File.ReadAllText(metaPath);
                            if (metaContents.Contains(searchGuid))
                            {
                                referencers.Add(assetPath);
                                didAdd = true;
                            }
                        }
                    });

                while (!result.IsCompleted)
                {
                    UnityEditor.EditorUtility.DisplayProgressBar(
                        "Finding Assets",
                        "...",
                        0f);
                }

                return referencers.ToArray();
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