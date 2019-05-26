using BeardPhantom.UCL.Attributes;
using UnityEngine;

namespace BeardPhantom.UCL.Assets
{
    /// <summary>
    /// Represents an audio event. A clip will be randomly selected on playback, using the specified AudioSourceSettings
    /// </summary>
    [CustomAssetCreateMenu]
    public class AudioCueAsset : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private AudioSourceSettings _settings;

        [SerializeField]
        private AudioClip[] _audio;

        #endregion

        #region Methods

        /// <summary>
        /// Play cue using source.
        /// </summary>
        public AudioClip Play(AudioSource source, bool loop = false)
        {
            _settings.ApplyTo(source);
            source.loop = loop;
            source.clip = _audio[Random.Range(0, _audio.Length)];
            source.Play();
            return source.clip;
        }

        #endregion
    }
}