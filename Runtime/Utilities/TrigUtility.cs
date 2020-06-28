using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class TrigUtility
    {
        #region Methods

        public static float NormalizeAngle(float x, float a, float b)
        {
            var width = b - a;
            var offsetValue = x - a;

            return offsetValue - Mathf.Floor(offsetValue / width) * width + a;
        }

        public static float NormalizeAngle(float x)
        {
            return NormalizeAngle(x, 0f, 360f);
        }

        public static bool IsBetweenAngles(float x, float a, float b)
        {
            a = NormalizeAngle(a);
            b = NormalizeAngle(b);
            x = NormalizeAngle(x);

            var aB = Mathf.Abs(Mathf.DeltaAngle(a, b));

            if (aB == 180f)
            {
                return x <= 180f;
            }

            var xA = Mathf.Abs(Mathf.DeltaAngle(x, a));
            var xB = Mathf.Abs(Mathf.DeltaAngle(x, b));

            return Mathf.Max(xA, xB) <= aB;
        }

        public static Vector2 DirectionFromAngle2D(float degrees)
        {
            return new Vector2(
                Mathf.Cos(degrees * Mathf.Deg2Rad),
                Mathf.Sin(degrees * Mathf.Deg2Rad));
        }

        public static Vector3 DirectionFromAngle(float degrees)
        {
            return new Vector3(
                Mathf.Sin(degrees * Mathf.Deg2Rad),
                0f,
                Mathf.Cos(degrees * Mathf.Deg2Rad));
        }

        #endregion
    }
}