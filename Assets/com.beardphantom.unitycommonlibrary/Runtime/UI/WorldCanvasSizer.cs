using UnityEngine;

namespace BeardPhantom.UCL.UI
{
    [ExecuteInEditMode]
    public class WorldCanvasSizer : MonoBehaviour
    {
        #region Fields

        public float Meters;

        public Vector2 Resolution;

        private RectTransform _rect;

        #endregion

        #region Methods

        public void RefreshCanvasSize()
        {
            if (_rect != null)
            {
                _rect.sizeDelta = Resolution;
                var scale = Meters / Resolution.x;
                _rect.localScale = new Vector3(scale, scale, 1f);
            }
        }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            RefreshCanvasSize();
        }

        private void OnValidate()
        {
            RefreshCanvasSize();
        }

        private void Reset()
        {
            Resolution = new Vector2(1920f, 1080f);
            Meters = 3f;
            RefreshCanvasSize();
        }

        #endregion
    }
}