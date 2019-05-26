using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Reference to a scene asset.
    /// </summary>
    [Serializable]
    public class SceneReference
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Path provided at build time.
        /// </summary>
        [field: SerializeField]
        public string RawPath { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a scene reference from a scene struct.
        /// </summary>
        public static SceneReference Create(Scene scene)
        {
            return Create(scene.path);
        }

        /// <summary>
        /// Create a scene reference from a full scene path.
        /// </summary>
        public static SceneReference Create(string scenePath)
        {
            return new SceneReference
            {
                RawPath = scenePath
            };
        }

        /// <summary>
        /// Get a path string that can be used by SceneManager.LoadScene
        /// </summary>
        public string GetLoadablePath()
        {
            var extensionIndex = RawPath.LastIndexOf(".unity");
            var prefixLength = "Assets/".Length;
            return RawPath.Substring(prefixLength, extensionIndex - prefixLength);
        }

        /// <summary>
        /// Get just the name of the scene
        /// </summary>
        public string GetSceneName()
        {
            return Path.GetFileNameWithoutExtension(RawPath);
        }

        /// <summary>
        /// Get the scene build index for the scene.
        /// </summary>
        public int GetSceneBuildIndex()
        {
            return SceneUtility.GetBuildIndexByScenePath(RawPath);
        }

        /// <summary>
        /// Implicity get raw path
        /// </summary>
        public static implicit operator string(SceneReference scene)
        {
            return scene.RawPath;
        }

        /// <summary>
        /// Implicity get build index
        /// </summary>
        public static implicit operator int(SceneReference scene)
        {
            return scene.GetSceneBuildIndex();
        }

        #endregion
    }
}