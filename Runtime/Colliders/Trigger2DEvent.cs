using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    public struct Trigger2DEvent
    {
        #region Fields

        public readonly Collider2DEventTransmitter Src;

        public readonly Collider2D Trigger;

        #endregion

        #region Constructors

        public Trigger2DEvent(Collider2DEventTransmitter src, Collider2D trigger)
        {
            Src = src;
            Trigger = trigger;
        }

        #endregion
    }
}