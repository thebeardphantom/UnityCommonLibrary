using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public class TestMaterialGenerator : ScriptableWizard
    {
        #region Fields

        public static Color32 Primary = new Color32(255, 255, 255, 255);

        public static List<Vector2> Scales = new List<Vector2>();

        public static Color32 Secondary = new Color32(190, 190, 190, 255);

        private static bool _isFoldout = true;

        private static string _saveFolder;

        private static string _projRelativeSaveFolder;

        private static string _filename = "TestMaterial";

        private static TestMaterialGenerator _wizard;

        private Texture2D _texture;

        #endregion

        #region Methods

        [MenuItem("Assets/Create/Test Materials")]
        private static void CreateWizard()
        {
            _wizard = DisplayWizard<TestMaterialGenerator>(
                "Create Test Material",
                "Create");

            if (Scales.Count == 0)
            {
                Scales.Add(Vector2.one);
                Scales.Add(Vector2.one * 2f);
                Scales.Add(Vector2.one * 5f);
            }

            if (_saveFolder == null)
            {
                _saveFolder = Application.dataPath;
            }
        }

        private static void DrawFileInfo()
        {
            if (GUILayout.Button("Browse"))
            {
                var newSaveFolder =
                    UnityEditor.EditorUtility.SaveFolderPanel(
                        "",
                        _projRelativeSaveFolder,
                        "");

                _saveFolder = string.IsNullOrEmpty(newSaveFolder)
                    ? _saveFolder
                    : newSaveFolder;

                GUI.changed = true;
            }

            _projRelativeSaveFolder =
                FileUtil.GetProjectRelativePath(_saveFolder);

            GUI.enabled = false;
            EditorGUILayout.TextField("Save Folder", _projRelativeSaveFolder);
            GUI.enabled = true;

            _filename = EditorGUILayout.TextField("Filename", _filename);
        }

        private static void DrawColorFields()
        {
            Primary = EditorGUILayout.ColorField("Primary", Primary);
            Secondary = EditorGUILayout.ColorField("Secondary", Secondary);
        }

        private static void DrawScales()
        {
            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "Scales");

            if (_isFoldout)
            {
                EditorGUI.indentLevel = 1;
                var newSize = EditorGUILayout.IntField("Size", Scales.Count);

                if (newSize != Scales.Count)
                {
                    ResizeList(Scales, newSize);
                }

                for (var i = 0; i < Scales.Count; i++)
                {
                    Scales[i] = EditorGUILayout.Vector2Field(
                        "Element " + i,
                        Scales[i]);
                }

                EditorGUI.indentLevel = 0;
            }
        }

        private static void ResizeList<T>(List<T> list, int count)
        {
            count = Mathf.Clamp(count, 0, int.MaxValue);

            while (list.Count > count)
            {
                list.RemoveAt(list.Count - 1);
            }

            while (list.Count < count)
            {
                list.Add(
                    list.Count > 0
                        ? list[list.Count - 1]
                        : default);
            }
        }

        protected override bool DrawWizardGUI()
        {
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            DrawFileInfo();
            DrawColorFields();
            DrawScales();
            UpdateValidity();

            return EditorGUI.EndChangeCheck();
        }

        private void CreateMaterials()
        {
            var distinct = Scales.Distinct();

            foreach (var s in distinct)
            {
                var newMat =
                    new Material(Shader.Find("Standard"))
                    {
                        mainTexture = _texture,
                        mainTextureScale = s
                    };

                AssetDatabase.CreateAsset(
                    newMat,
                    $"{_projRelativeSaveFolder}/{_filename} {s.x}x{s.y}.mat");
            }
        }

        private void CreateTexture()
        {
            var texPath = $"{_saveFolder}/{_filename}.png";

            // Clamp alpha
            Primary.a = Secondary.a = 255;

            // Create texture
            _texture = new Texture2D(2, 2)
            {
                filterMode = FilterMode.Point
            };

            _texture.SetPixels32(
                new[]
                {
                    Primary,
                    Secondary,
                    Secondary,
                    Primary
                });

            _texture.Apply();

            // Write to path
            File.WriteAllBytes(texPath, _texture.EncodeToPNG());
            DestroyImmediate(_texture);
            AssetDatabase.Refresh();

            texPath = $"{_projRelativeSaveFolder}/{_filename}.png";

            _texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);

            // Change import settings
            var importer = AssetImporter.GetAtPath(texPath) as TextureImporter;
            importer.filterMode = FilterMode.Point;
            importer.maxTextureSize = 32;

            importer.textureCompression =
                TextureImporterCompression.Uncompressed;

            importer.wrapMode = TextureWrapMode.Repeat;
            importer.textureType = TextureImporterType.Default;
            importer.SaveAndReimport();
        }

        private void OnWizardCreate()
        {
            CreateTexture();
            CreateMaterials();
        }

        private void UpdateValidity()
        {
            if (string.IsNullOrEmpty(_projRelativeSaveFolder))
            {
                errorString = "Must be saved in project.";
                isValid = false;
            }
            else
            {
                errorString = "";
                isValid = true;
            }
        }

        #endregion
    }
}