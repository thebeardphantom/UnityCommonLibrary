﻿using BeardPhantom.UCL.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace BeardPhantom.UCL
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LayoutElement))]
    public class InferSize : MonoBehaviour
    {
        #region Fields

        public Vector2 FlexibleSizeOffset;

        public Vector2 MinSizeOffset;

        public Vector2 PreferredSizeOffset;

        public Vector2 RectSizeOffset;

        public Graphic TargetGraphic;

        public bool UseFlexibleSize;

        public bool UseMinSize;

        public bool UsePreferredSize;

        public bool UseRectSize;

        private LayoutElement _layout;

        private RectTransform _rt;

        #endregion

        #region Methods

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _layout = GetComponent<LayoutElement>();
        }

        private void Update()
        {
            if (TargetGraphic != null)
            {
                if (UseRectSize)
                {
                    _rt.SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Horizontal,
                        TargetGraphic.rectTransform.rect.width + RectSizeOffset.x);

                    _rt.SetSizeWithCurrentAnchors(
                        RectTransform.Axis.Vertical,
                        TargetGraphic.rectTransform.rect.height + RectSizeOffset.y);
                }

                if (UseMinSize)
                {
                    _layout.minWidth =
                        LayoutUtility.GetMinWidth(TargetGraphic.rectTransform) + MinSizeOffset.x;

                    _layout.minHeight =
                        LayoutUtility.GetMinHeight(TargetGraphic.rectTransform) + MinSizeOffset.y;
                }

                if (UsePreferredSize)
                {
                    _layout.preferredWidth =
                        LayoutUtility.GetPreferredWidth(TargetGraphic.rectTransform) + PreferredSizeOffset.x;

                    _layout.preferredHeight =
                        LayoutUtility.GetPreferredHeight(TargetGraphic.rectTransform) + PreferredSizeOffset.y;
                }

                if (UseFlexibleSize)
                {
                    _layout.flexibleWidth =
                        LayoutUtility.GetFlexibleWidth(TargetGraphic.rectTransform) + FlexibleSizeOffset.x;

                    _layout.flexibleHeight =
                        LayoutUtility.GetFlexibleHeight(TargetGraphic.rectTransform) + FlexibleSizeOffset.y;
                }
            }
        }

        #endregion
    }
}