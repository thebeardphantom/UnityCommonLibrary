using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class RectUtility
    {
        #region Methods

        public static Vector2 ClosestPointOnRect(this Rect rect, Vector2 point)
        {
            var adjusted = Vector2.zero;
            adjusted.x = Mathf.Clamp(point.x, rect.xMin, rect.xMax);
            adjusted.y = Mathf.Clamp(point.y, rect.yMin, rect.yMax);

            return adjusted;
        }

        #endregion
    }
}