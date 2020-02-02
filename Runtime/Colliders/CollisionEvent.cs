using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    public struct CollisionEvent
    {
        #region Fields

        public readonly ColliderEventTransmitter Src;

        public readonly Collision Collision;

        #endregion

        #region Constructors

        public CollisionEvent(ColliderEventTransmitter src, Collision collision)
        {
            Src = src;
            Collision = collision;
        }

        #endregion
    }
}