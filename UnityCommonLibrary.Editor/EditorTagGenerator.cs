using BeardPhantom.UCL.Utility;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    [InitializeOnLoad]
    public static class EditorTagGenerator
    {
        #region Fields

        public const string AUTO_TAG_CS_GENERATION_KEY = "{0}_AUTO_TAG_CS_GENERATION";

        private const string TAG_CLASS_FORMAT = @"
namespace SpaceTech.Game
{{
    public static class Tags
    {{
{0}
    }}
}}";

        private const string TAG_FORMAT = "        public const string {0} = \"{1}\";";

        #endregion

        #region Constructors

        static EditorTagGenerator()
        {
            void CheckThenGenerate(object arg)
            {
                var key = GetAutoGenerateEditorPrefsKey();
                if (EditorPrefs.GetBool(key, false))
                {
                    GenerateTagList();
                }
            }

            CompilationPipeline.compilationStarted -= CheckThenGenerate;
            CompilationPipeline.compilationStarted += CheckThenGenerate;

            CheckThenGenerate(null);
        }

        #endregion

        #region Methods

        public static void SetAutoGenerateTagCSFile(bool allow)
        {
            var key = GetAutoGenerateEditorPrefsKey();
            EditorPrefs.SetBool(key, allow);
        }

        public static string GetAutoGenerateEditorPrefsKey()
        {
            return AUTO_TAG_CS_GENERATION_KEY.FormatStr(Application.productName);
        }

        [MenuItem("Assets/Generate Tag List")]
        private static void GenerateTagList()
        {
            if (Application.isPlaying)
            {
                return;
            }

            var tags = InternalEditorUtility.tags;
            var tagList = tags.Select(
                    tag =>
                    {
                        var varName = GetVarName(tag);
                        return string.Format(TAG_FORMAT, varName, tag);
                    })
                .ToArray();
            var contents = string.Format(TAG_CLASS_FORMAT, string.Join("\n\n", tagList));

            contents = contents.Replace("\r\n", "\n");
            var path = Path.Combine(Application.dataPath, "Scripts", "Game");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "Tags.cs");
            File.WriteAllText(path, contents);
            AssetDatabase.Refresh();
        }

        private static string GetVarName(string tag)
        {
            return string.Concat(
                tag.Select(
                    (x, i) => i > 0 && char.IsUpper(x)
                        ? $"_{x.ToString().ToUpper()}"
                        : x.ToString().ToUpper()));
        }

        #endregion
    }
}