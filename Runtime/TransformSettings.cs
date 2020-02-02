using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Stores the configuration of a Transform.
    /// </summary>
    public class TransformSettings
    {
        #region Properties

        public Vector3 LocalScale { get; }

        public Transform Parent { get; }

        public Vector3 LocalPosition { get; }

        public Quaternion LocalRotation { get; }

        public int SiblingIndex { get; }

        #endregion

        #region Constructors

        public TransformSettings(Transform transform)
        {
            Parent = transform.parent;
            SiblingIndex = transform.GetSiblingIndex();
            LocalPosition = transform.localPosition;
            LocalRotation = transform.localRotation;
            LocalScale = transform.localScale;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Apply settings to a Transform.
        /// </summary>
        public void ApplyTo(Transform transform)
        {
            transform.parent = Parent;
            transform.SetSiblingIndex(SiblingIndex);
            transform.localPosition = LocalPosition;
            transform.localRotation = LocalRotation;
            transform.localScale = LocalScale;
        }

        #endregion
    }

    /// <summary>
    /// Stores the configuration of a RectTransform.
    /// </summary>
    public class RectTransformSettings : TransformSettings
    {
        #region Properties

        public Vector3 AnchoredPosition3D { get; }

        public Vector2 AnchorMax { get; }

        public Vector2 AnchorMin { get; }

        public Vector2 Pivot { get; }

        public Vector2 SizeDelta { get; }

        #endregion

        #region Constructors

        public RectTransformSettings(RectTransform rect) : base(rect)
        {
            AnchoredPosition3D = rect.anchoredPosition3D;
            AnchorMin = rect.anchorMin;
            AnchorMax = rect.anchorMax;
            SizeDelta = rect.sizeDelta;
            Pivot = rect.pivot;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Apply settings to another RectTransform
        /// </summary>
        public void ApplyTo(RectTransform rect)
        {
            base.ApplyTo(rect);
            rect.anchorMin = AnchorMin;
            rect.anchorMax = AnchorMax;
            rect.pivot = Pivot;
            rect.anchoredPosition3D = AnchoredPosition3D;
            rect.sizeDelta = SizeDelta;
        }

        #endregion
    }
}