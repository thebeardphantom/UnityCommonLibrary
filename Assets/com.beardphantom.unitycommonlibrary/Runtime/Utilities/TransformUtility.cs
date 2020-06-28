using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class TransformUtility
    {
        #region Methods

        public static void Reset(this Transform t)
        {
            Reset(t, TransformElement.All, Space.World);
        }

        public static void Reset(this Transform t, TransformElement elements)
        {
            Reset(t, elements, Space.World);
        }

        public static void Reset(this Transform t, Space space)
        {
            Reset(t, TransformElement.All, space);
        }

        public static void Reset(
            this Transform t,
            TransformElement elements,
            Space space)
        {
            if ((elements & TransformElement.Position) != 0)
            {
                if (space == Space.World)
                {
                    t.position = Vector3.zero;
                }
                else
                {
                    t.localPosition = Vector3.zero;
                }
            }

            if ((elements & TransformElement.Rotation) != 0)
            {
                if (space == Space.World)
                {
                    t.rotation = Quaternion.identity;
                }
                else
                {
                    t.localRotation = Quaternion.identity;
                }
            }

            if ((elements & TransformElement.Scale) != 0)
            {
                t.localScale = Vector3.one;
            }
        }

        public static void Match(
            this Transform t,
            Transform other,
            TransformElement elements)
        {
            if ((elements & TransformElement.Position) != 0)
            {
                t.position = other.position;
            }

            if ((elements & TransformElement.Rotation) != 0)
            {
                t.rotation = other.rotation;
            }

            if ((elements & TransformElement.Scale) != 0)
            {
                t.localScale = other.localScale;
            }
        }

        public static Vector3 DirectionTo(this Transform t, Transform other)
        {
            return (other.position - t.position).normalized;
        }

        public static Vector3 DirectionTo(this Transform t, Vector3 other)
        {
            return (other - t.position).normalized;
        }

        public static Vector3 DirectionTo(this Vector3 v, Vector3 other)
        {
            return (other - v).normalized;
        }

        public static Vector3 DirectionTo(this Vector3 v, Transform other)
        {
            return (other.position - v).normalized;
        }

        #endregion
    }
}