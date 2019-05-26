using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// An object to store the setup of an AudioSource
    /// </summary>
    [Serializable]
    public class AudioSourceSettings
    {
        #region Fields

        [SerializeField]
        private AudioMixerGroup _mixerGroup;

        [SerializeField]
        [Header("Bypass")]
        private bool _bypassReverbZones;

        [SerializeField]
        private bool _bypassEffects;

        [SerializeField]
        private bool _bypassListenerEffects;

        [Range(0, 256)]
        [SerializeField]
        private int _priority = 128;

        [Range(0f, 1f)]
        [SerializeField]
        [Header("Volume")]
        private float _minVolume = 1f;

        [Range(0f, 1f)]
        [SerializeField]
        private float _maxVolume = 1f;

        [Range(-3f, 3f)]
        [SerializeField]
        [Header("Pitch")]
        private float _minPitch = 1f;

        [Range(-3f, 3f)]
        [SerializeField]
        private float _maxPitch = 1f;

        [Range(-1f, 1f)]
        [SerializeField]
        private float _panStereo;

        [Range(0f, 1f)]
        [SerializeField]
        private float _spatialBlend;

        [Range(0f, 1f)]
        [SerializeField]
        private float _reverbZoneMix;

        [Range(0f, 5f)]
        [SerializeField]
        [Header("3D Settings")]
        private float _dopplerLevel = 1f;

        [Range(0f, 360f)]
        [SerializeField]
        private float _spread;

        [SerializeField]
        private AudioRolloffMode _rolloffMode;

        [SerializeField]
        [Header("Distance")]
        private float _minDistance = 1f;

        [SerializeField]
        private float _maxDistance = 500f;

        #endregion

        #region Methods

        /// <summary>
        /// Get settings from an AudioSource
        /// </summary>
        public static AudioSourceSettings GetFromSource(AudioSource source)
        {
            return new AudioSourceSettings
            {
                _minPitch = source.pitch,
                _maxPitch = source.pitch,
                _minVolume = source.volume,
                _maxVolume = source.volume,
                _minDistance = source.minDistance,
                _maxDistance = source.maxDistance,
                _reverbZoneMix = source.reverbZoneMix,
                _spatialBlend = source.spatialBlend,
                _mixerGroup = source.outputAudioMixerGroup,
                _dopplerLevel = source.dopplerLevel,
                _panStereo = source.panStereo,
                _priority = source.priority,
                _spread = source.spread,
                _rolloffMode = source.rolloffMode,
                _bypassEffects = source.bypassEffects,
                _bypassListenerEffects = source.bypassListenerEffects,
                _bypassReverbZones = source.bypassReverbZones
            };
        }

        /// <summary>
        /// Copy settings from source to destination
        /// </summary>
        public static void CopyFrom(AudioSource source, AudioSource destination)
        {
            var settings = GetFromSource(source);
            settings.ApplyTo(destination);
        }

        /// <summary>
        /// Apply settings to an AudioSource
        /// </summary>
        public void ApplyTo(AudioSource source)
        {
            source.pitch = Random.Range(_minPitch, _maxPitch);
            source.volume = Random.Range(_minVolume, _maxVolume);

            // Distance
            source.minDistance = _minDistance;
            source.maxDistance = _maxDistance;

            source.reverbZoneMix = _reverbZoneMix;
            source.spatialBlend = _spatialBlend;
            source.outputAudioMixerGroup = _mixerGroup;
            source.dopplerLevel = _dopplerLevel;
            source.panStereo = _panStereo;
            source.priority = _priority;
            source.spread = _spread;
            source.rolloffMode = _rolloffMode;

            // Bypass
            source.bypassEffects = _bypassEffects;
            source.bypassListenerEffects = _bypassListenerEffects;
            source.bypassReverbZones = _bypassReverbZones;
        }

        #endregion
    }
}