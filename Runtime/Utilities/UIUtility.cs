using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BeardPhantom.UCL.Utility
{
    public static class UIUtility
    {
        #region Methods

        public static void SetVisible(this CanvasGroup group, bool active)
        {
            if (!group.gameObject.activeSelf && active)
            {
                group.gameObject.SetActive(true);
            }

            group.alpha = active
                ? 1f
                : 0f;
            group.blocksRaycasts = active;
        }

        public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            var center = rectTransform.GetWorldCenter();
            rectTransform.pivot = pivot;
            Vector3 offset = center - rectTransform.GetWorldCenter();
            rectTransform.position += offset;
        }

        public static void SetAnchors(this RectTransform rectTransform, Vector2 anchor)
        {
            rectTransform.anchorMin = anchor;
            rectTransform.anchorMax = anchor;
        }

        public static void SetAnchors(this RectTransform rectTransform, Vector2 min, Vector2 max)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
        }

        public static void SetToFill(this RectTransform rectTransform, Vector2 anchor)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
        }

        public static Vector2 GetWorldCenter(this RectTransform rectTransform)
        {
            return rectTransform.TransformPoint(rectTransform.rect.center);
        }

        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            return new Rect(
                rectTransform.TransformPoint(rectTransform.rect.min),
                rectTransform.rect.size);
        }

        public static void AddCallback(
            this EventTrigger evt,
            EventTriggerType eventId,
            UnityAction<BaseEventData> callback)
        {
            var triggerEvt = new EventTrigger.TriggerEvent();
            triggerEvt.AddListener(callback);
            evt.triggers.Add(
                new EventTrigger.Entry
                {
                    callback = triggerEvt,
                    eventID = eventId
                });
        }

        #endregion
    }
}