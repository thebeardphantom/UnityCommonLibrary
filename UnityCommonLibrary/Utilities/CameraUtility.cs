using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class CameraUtility
    {
        #region Methods

        public static Bounds OrthographicBounds(this Camera camera)
        {
            var camHeight = camera.orthographicSize * 2f;

            return new Bounds(
                camera.transform.position,
                new Vector3(camHeight * camera.aspect, camHeight, 0));
        }

        #endregion
    }
}