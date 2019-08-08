using System;
using BeardPhantom.UCL.Attributes;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace BeardPhantom.UCL.Assets
{
    /// <summary>
    /// Represents an audio event. A clip will be randomly selected on playback, using the specified AudioSourceSettings
    /// </summary>
    [CustomAssetCreateMenu]
    public class AudioCueAsset : ScriptableObject
    {
        #region Types

        [Serializable]
        public class AudioData
        {
            #region Fields

            public AudioClip Clip;

            public float VolumeOffset;

            #endregion
        }

        #endregion

        #region Fields

        [SerializeField]
        private AudioSourceSettings _settings;

        [SerializeField]
        private AudioData[] _audio;

        #endregion

        #region Properties

        public AudioData[] Audio => _audio;

        #endregion

        #region Methods

        /// <summary>
        /// Play cue using source.
        /// </summary>
        public AudioClip Play(AudioSource source, bool loop = false)
        {
            Assert.IsTrue(Audio.Length > 0, $"No assigned audio for {name}");
            _settings.ApplyTo(source);
            var selectedIndex = Random.Range(0, Audio.Length);
            var clip = Audio[selectedIndex];
            Assert.IsNotNull(clip.Clip, $"Audio entry {selectedIndex} for {name} has no assigned clip");
            source.volume += clip.VolumeOffset;
            source.loop = loop;
            source.clip = clip.Clip;
            source.Play();
            return source.clip;
        }

        #endregion
    }
}