using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.UCL.Utility
{
    public static class TransformUtility
    {
        #region Fields

        private static readonly Queue<Transform> _bfsSearchQueue =
            new Queue<Transform>();

        #endregion

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

        public static Transform FindChildBfs(
            this Transform t,
            string search,
            StringComparison comparison = StringComparison.CurrentCulture,
            bool tag = false)
        {
            _bfsSearchQueue.Clear();

            for (var i = 0; i < t.childCount; i++)
            {
                _bfsSearchQueue.Enqueue(t.GetChild(i));
            }

            Transform found = null;

            while (_bfsSearchQueue.Count > 0)
            {
                var child = _bfsSearchQueue.Dequeue();

                if (tag && child.tag.Equals(search, comparison) || child.name.Equals(search, comparison))
                {
                    found = child;

                    break;
                }

                for (var i = 0; i < child.childCount; i++)
                {
                    _bfsSearchQueue.Enqueue(child.GetChild(i));
                }
            }

            _bfsSearchQueue.Clear();

            return found;
        }

        public static Transform FindChildDfs(
            this Transform t,
            string search,
            StringComparison comparison = StringComparison.CurrentCulture,
            bool tag = false)
        {
            for (var i = 0; i < t.childCount; i++)
            {
                var child = t.GetChild(i);

                if (tag && child.tag.Equals(search, comparison) || child.name.Equals(search, comparison))
                {
                    return child;
                }

                child = FindChildDfs(t.GetChild(i), search);

                if (child)
                {
                    return child;
                }
            }

            return null;
        }

        public static Transform FindParent(
            this Transform t,
            string search,
            StringComparison comparison = StringComparison.CurrentCulture,
            bool tag = false)
        {
            var transform = t;

            while (transform)
            {
                if (tag && transform.tag.Equals(search, comparison) || transform.name.Equals(search, comparison))
                {
                    return transform;
                }

                transform = transform.parent;
            }

            return null;
        }

        public static Transform[] GetChildren(this Transform transform)
        {
            var array = new Transform[transform.childCount];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = transform.GetChild(i);
            }

            return array;
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        #endregion
    }
}