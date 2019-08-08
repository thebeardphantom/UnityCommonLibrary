using BeardPhantom.UCL.Assets;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomEditor(typeof(AudioCueAsset))]
    public class AudioCueAssetEditor : UnityEditor.Editor
    {
        #region Fields

        private AudioSource _lastPlayingSource;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var cachedColor = GUI.color;
            if (_lastPlayingSource == null)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("Play"))
                {
                    _lastPlayingSource = EditorAudioUtility.Play((AudioCueAsset) target);
                }
            }
            else
            {
                GUI.color = Color.red;
                if (GUILayout.Button("Stop"))
                {
                    _lastPlayingSource.Stop();
                }
            }

            GUI.color = cachedColor;

            DrawPropertiesExcluding(serializedObject, "m_Script");
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        #endregion
    }
}