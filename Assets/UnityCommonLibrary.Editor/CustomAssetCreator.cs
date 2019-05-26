using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeardPhantom.UCL.Attributes;
using BeardPhantom.UCL.Utility;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    /// <summary>
    /// Custom menu for creating custom assets
    /// </summary>
    public class CustomAssetCreator : EditorWindow
    {
        #region Fields

        /// <summary>
        /// Search filtered results
        /// </summary>
        private readonly List<Type> _filteredResults = new List<Type>();

        /// <summary>
        /// Found data types
        /// </summary>
        private Type[] _data = new Type[0];

        /// <summary>
        /// GUI search field
        /// </summary>
        private SearchField _searchField;

        /// <summary>
        /// Current position in scroll
        /// </summary>
        private Vector2 _scrollValue;

        /// <summary>
        /// Selected element in list
        /// </summary>
        private int _selection = -1;

        #endregion

        #region Methods

        /// <summary>
        /// Shows asset creator window
        /// </summary>
        [MenuItem("Assets/Create Custom...", priority = int.MinValue)]
        private static void ShowAssetCreator()
        {
            GetWindow<CustomAssetCreator>(true, "Custom Asset Creator", true);
        }

        /// <summary>
        /// Is type valid for adding to this window
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static bool IsValidType(Type t)
        {
            return !t.IsAbstract &&
                t.IsPublic &&
                typeof(ScriptableObject).IsAssignableFrom(t) &&
                (t.GetCustomAttribute<CreateAssetMenuAttribute>(true) != null || t.GetCustomAttribute<CustomAssetCreateMenuAttribute>(true) != null);
        }

        /// <summary>
        /// Get selected asset folder
        /// </summary>
        /// <returns></returns>
        private static string GetSelectedFolder()
        {
            var obj = Selection.activeObject;
            if (obj == null)
            {
                return "Assets";
            }

            var path = AssetDatabase.GetAssetPath(obj);
            return AssetDatabase.IsValidFolder(path) ? path : Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Find valid types
        /// </summary>
        private void OnEnable()
        {
            wantsMouseMove = true;
            _data = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypesSafe())
                .Where(IsValidType)
                .OrderBy(t => t.Name)
                .ToArray();
            _filteredResults.AddRange(_data);
            _searchField = new SearchField();
        }

        /// <summary>
        /// Draw window
        /// </summary>
        private void OnGUI()
        {
            if (_searchField.OnGUI(false))
            {
                _filteredResults.Clear();
                _filteredResults.AddRange(_data.Where(t => _searchField.FilterValue.MatchesValue(t.Name) || _searchField.FilterValue.MatchesKeywords(t.Name, true)));
            }

            _searchField.Focus();

            if (Event.current.type == EventType.KeyUp)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.UpArrow:
                    {
                        _selection--;
                        Repaint();
                        break;
                    }
                    case KeyCode.DownArrow:
                    {
                        _selection++;
                        Repaint();
                        break;
                    }
                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                    {
                        if (_selection > 0 && _selection < _filteredResults.Count)
                        {
                            CreateObject(_filteredResults[_selection]);
                        }

                        break;
                    }
                }
            }

            _selection = Mathf.Clamp(_selection, 0, _filteredResults.Count - 1);

            using (var scroll = new EditorGUILayout.ScrollViewScope(_scrollValue))
            {
                _scrollValue = scroll.scrollPosition;

                for (var i = 0; i < _filteredResults.Count; i++)
                {
                    var result = _filteredResults[i];
                    var color = GUI.color;
                    if (_selection == i)
                    {
                        GUI.color = new Color(0.5f, 1f, 0.83f);
                    }

                    if (GUILayout.Button(result.Name, GUI.skin.box, GUILayout.ExpandWidth(true)))
                    {
                        CreateObject(result);
                    }

                    GUI.color = color;
                    var rect = GUILayoutUtility.GetLastRect();
                    if (rect.Contains(Event.current.mousePosition) && Event.current.delta.sqrMagnitude > 0)
                    {
                        _selection = i;
                        Repaint();
                    }
                }
            }
        }

        /// <summary>
        /// Creates the selected type and saves it to the project
        /// </summary>
        /// <param name="type"></param>
        private void CreateObject(Type type)
        {
            var path = UnityEditor.EditorUtility.SaveFilePanelInProject(
                "Save Asset",
                type.Name,
                "asset",
                "Save new asset",
                GetSelectedFolder());

            if (!string.IsNullOrWhiteSpace(path))
            {
                var newAsset = ObjectFactory.CreateInstance(type);
                AssetDatabase.CreateAsset(newAsset, path);
                AssetDatabase.Refresh();
                Selection.activeObject = newAsset;
                Close();
            }
        }

        #endregion
    }
}