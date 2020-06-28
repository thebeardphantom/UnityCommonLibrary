using UnityEngine;

namespace BeardPhantom.UCL
{
    public delegate TResult ValueEventArgs<in TArgs, out TResult>(TArgs args) where TArgs : struct;

    public delegate void ValueEventHandler<in TArgs>(TArgs args) where TArgs : struct;

    /// <summary>
    /// Centralizes logging for UCL.
    /// </summary>
    public static class UCLCore
    {
        #region Events

        public static event ValueEventArgs<InstantiatePrefabEventArgs, GameObject> ObjectCreated =
            DefaultInstantiatePrefabEventHandler;

        #endregion

        #region Fields

        /// <summary>
        /// How to log from UCL classes
        /// </summary>
        public static ILogger Logger = Debug.unityLogger;

        #endregion

        #region Methods

        internal static GameObject Instantiate(InstantiatePrefabEventArgs args)
        {
            return ObjectCreated.Invoke(args);
        }

        private static GameObject DefaultInstantiatePrefabEventHandler(InstantiatePrefabEventArgs args)
        {
            return Object.Instantiate(args.Prefab, args.Position, args.Rotation, args.Parent);
        }

        #endregion
    }

    public struct InstantiatePrefabEventArgs
    {
        #region Fields

        public GameObject Prefab;

        public Quaternion Rotation;

        public Vector3 Position;

        public Transform Parent;

        #endregion

        #region Constructors

        public InstantiatePrefabEventArgs(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }

        #endregion
    }
}