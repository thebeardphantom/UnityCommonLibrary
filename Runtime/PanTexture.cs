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
                _material = Instantiate(rawImage.material);
                rawImage.material = _material;
                return;
            }

            var image = GetComponent<Image>();
            if (image)
            {
                _material = Instantiate(image.material);
                image.material = _material;
            }
        }

        private void OnDestroy()
        {
            Destroy(_material);
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