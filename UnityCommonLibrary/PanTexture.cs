using BeardPhantom.UCL.Time;
using BeardPhantom.UCL.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Utility behaviour for panning a texture's UVs.
    /// </summary>
    public class PanTexture : MonoBehaviour
    {
        #region Fields

        public Vector2 Speed;

        public TimeMode TimeMode;

        private Material _material;

        private Vector2 _offset;

        #endregion

        #region Methods

        private void Awake()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer)
            {
                _material = renderer.material;
                return;
            }

            var rawImage = GetComponent<RawImage>();
            if (rawImage)
            {
#if UNITY_EDITOR
                _material = Instantiate(rawImage.material);
                rawImage.material = _material;
#else
                _material = rawImage.material;
#endif
                return;
            }

            var image = GetComponent<Image>();
            if (image)
            {
#if UNITY_EDITOR
                _material = Instantiate(image.material);
                image.material = _material;
#else
                _material = image.material;
#endif
            }
        }

        private void Update()
        {
            _offset += Speed * TimeUtility.GetCurrentTime(TimeMode);
            _offset.x %= 1f;
            _offset.y %= 1f;
            _material.SetTextureOffset("_MainTex", _offset);
        }

        #endregion
    }
}