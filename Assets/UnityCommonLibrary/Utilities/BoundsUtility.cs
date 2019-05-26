using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class BoundsUtility
    {
        #region Methods

        /// <summary>
        /// Returns true if this bounds.size >= target.size in all axes.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool CouldContain(this Bounds bounds, Bounds target)
        {
            var bSize = bounds.size;
            var tSize = target.size;

            return bSize.x >= tSize.x && bSize.y >= tSize.y && bSize.z >= tSize.z;
        }

        public static Vector3 RandomPointInside(this Bounds bounds)
        {
            return new Vector3
            {
                x = Random.Range(bounds.min.x, bounds.max.x),
                y = Random.Range(bounds.min.y, bounds.max.y),
                z = Random.Range(bounds.min.z, bounds.max.z)
            };
        }

        #endregion
    }
}