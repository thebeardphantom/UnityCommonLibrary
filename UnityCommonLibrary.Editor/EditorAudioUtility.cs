using System.Collections.Generic;
using BeardPhantom.UCL.Assets;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    [InitializeOnLoad]
    public static class EditorAudioUtility
    {
        #region Fields

        private static readonly List<AudioSource> _playingSources = new List<AudioSource>();

        #endregion

        #region Constructors

        static EditorAudioUtility()
        {
            EditorApplication.update -= EditorUpdate;
            EditorApplication.update += EditorUpdate;

            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
        }

        #endregion

        #region Methods

        public static AudioSource Play(AudioCueAsset cue)
        {
            var source = GetAudioSource();
            cue.Play(source);
            return source;
        }

        private static void OnPlaymodeStateChanged(PlayModeStateChange obj)
        {
            for (var i = _playingSources.Count - 1; i >= 0; i--)
            {
                var source = _playingSources[i];
                if (source != null)
                {
                    Object.DestroyImmediate(source.gameObject);
                }

                _playingSources.RemoveAt(i);
            }
        }

        private static AudioSource GetAudioSource()
        {
            var audioSource = UnityEditor.EditorUtility.CreateGameObjectWithHideFlags(
                    "AUDIOSOURCE",
                    HideFlags.HideAndDontSave,
                    typeof(AudioSource))
                .GetComponent<AudioSource>();
            _playingSources.Add(audioSource);
            return audioSource;
        }

        private static void EditorUpdate()
        {
            for (var i = _playingSources.Count - 1; i >= 0; i--)
            {
                var source = _playingSources[i];
                var nullSource = source == null;
                if (!nullSource && source.isPlaying)
                {
                    continue;
                }

                if (!nullSource)
                {
                    Object.DestroyImmediate(source.gameObject);
                }

                _playingSources.RemoveAt(i);
            }
        }

        #endregion
    }
}