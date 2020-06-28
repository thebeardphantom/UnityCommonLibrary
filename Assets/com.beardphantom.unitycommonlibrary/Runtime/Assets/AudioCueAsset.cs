using BeardPhantom.UCL.Utility;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace BeardPhantom.UCL.Assets
{
    /// <summary>
    /// Represents an audio event. A clip will be randomly selected on playback, using the specified AudioSourceSettings
    /// </summary>
    public class AudioCueAsset : ScriptableObject
    {
        #region Types

        [Serializable]
        public class AudioData : IWeightedChoice
        {
            #region Fields

            public AudioClip Clip;

            public float VolumeOffset;

            [SerializeField]
            private int _weight = 1;

            #endregion

            #region Properties

            /// <inheritdoc />
            public int Weight => _weight;

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
            var index = Audio.ChooseIndexFromWeighted();
            Assert.IsFalse(index < 0, "index < 0");

            var audio = Audio[index];
            Assert.IsNotNull(audio.Clip, $"Audio entry {index} for {name} has no assigned clip");

            source.volume += audio.VolumeOffset;
            source.loop = loop;
            source.clip = audio.Clip;

            source.Play();
            return source.clip;
        }

        #endregion
    }
}