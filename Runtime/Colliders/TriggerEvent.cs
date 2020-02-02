using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    public struct TriggerEvent
    {
        #region Fields

        public readonly ColliderEventTransmitter Src;

        public readonly Collider Trigger;

        #endregion

        #region Constructors

        public TriggerEvent(ColliderEventTransmitter src, Collider trigger)
        {
            Src = src;
            Trigger = trigger;
        }

        #endregion
    }
}