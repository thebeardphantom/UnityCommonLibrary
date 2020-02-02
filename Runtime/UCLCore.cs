using BeardPhantom.UCL.Signals;
using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Centralizes logging for UCL.
    /// </summary>
    public static class UCLCore
    {
        #region Fields

        public static readonly Signal<GameObject> ObjectCreated = new Signal<GameObject>();

        /// <summary>
        /// How to log from UCL classes
        /// </summary>
        public static ILogger Logger = Debug.unityLogger;

        #endregion

        #region Methods

        internal static GameObject Instantiate(
            GameObject original,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
        {
            var instance = Object.Instantiate(original, position, rotation, parent);
            ObjectCreated.Publish(instance);
            return instance;
        }

        internal static GameObject Instantiate(GameObject original, Transform parent)
        {
            var instance = Object.Instantiate(original, parent);
            ObjectCreated.Publish(instance);
            return instance;
        }

        #endregion
    }
}