using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BeardPhantom.UCL.Utility
{
    public static class RandomUtility
    {
        #region Methods

        public static Vector4 Vector4(float min, float max)
        {
            return new Vector4(
                Random.Range(min, max),
                Random.Range(min, max),
                Random.Range(min, max),
                Random.Range(min, max));
        }

        public static Vector3 Vector3(float min, float max)
        {
            return new Vector3(
                Random.Range(min, max),
                Random.Range(min, max),
                Random.Range(min, max));
        }

        public static Vector2 Vector2(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }

        public static Vector3 InsideBounds3D(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
        }

        public static Vector2 InsideBounds2D(this Bounds bounds)
        {
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));
        }

        public static Vector3 InsideBoundsInt3D(this BoundsInt bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x + 1),
                Random.Range(bounds.min.y, bounds.max.y + 1),
                Random.Range(bounds.min.z, bounds.max.z + 1));
        }

        public static Vector2 InsideBoundsInt2D(this BoundsInt bounds)
        {
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x + 1),
                Random.Range(bounds.min.y, bounds.max.y + 1));
        }

        public static float NormalRange()
        {
            return Random.Range(0f, 1f);
        }

        public static float FullRange()
        {
            return Random.Range(-1f, 1f);
        }

        public static float Range(Vector2 range)
        {
            return Random.Range(range.x, range.y);
        }

        public static T SelectRandomWeighted<T>(IEnumerable<T> collection, Func<T, float> getWeight)
        {
            var totalWeight = 0f;
            var minWeight = float.MaxValue;
            foreach (var value in collection)
            {
                var weight = getWeight(value);
                totalWeight += weight;
                minWeight = Mathf.Min(weight, minWeight);
            }

            var rng = Random.Range(minWeight, totalWeight);

            foreach (var value in collection)
            {
                rng -= getWeight(value);
                if (rng < 0f)
                {
                    return value;
                }
            }

            return default;
        }

        #endregion
    }
}