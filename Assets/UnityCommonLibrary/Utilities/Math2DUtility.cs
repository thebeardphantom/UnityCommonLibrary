using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class Math2DUtility
    {
        #region Methods

        public static float AngleRaw(Transform a, Transform b)
        {
            return AngleRaw(a.position, b.position);
        }

        public static float Angle(Transform a, Transform b)
        {
            return Angle(a.position, b.position);
        }

        public static float AngleRaw(Vector2 a, Vector2 b)
        {
            var dir = b - a;

            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        public static float Angle(Vector2 a, Vector2 b)
        {
            var angle = AngleRaw(a, b);

            return ToNormalized(AngleRaw(a, b));
        }

        /// <summary>
        /// Returns an angle from 0...360
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float ToNormalized(float angle)
        {
            if (Mathf.Sign(angle) == -1f)
            {
                angle = 360f - Mathf.Abs(angle);
            }

            return angle;
        }

        public static bool IsFacingRight(float angle)
        {
            angle = ToNormalized(angle);

            return angle > 90 && angle < 270;
        }

        public static void LookAt2D(this Transform t, Transform other)
        {
            LookAt2D(t, other.position);
        }

        public static void LookAt2D(this Transform t, Vector2 other)
        {
            var angle = Angle(t.position, other);
            t.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public static void SetForward2D(this Transform t, Vector2 forward)
        {
            t.right = forward;
        }

        #endregion
    }
}