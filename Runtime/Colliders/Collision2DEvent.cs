using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    public struct Collision2DEvent
    {
        #region Fields

        public readonly Collider2DEventTransmitter Src;

        public readonly Collision2D Collision;

        #endregion

        #region Constructors

        public Collision2DEvent(Collider2DEventTransmitter src, Collision2D collision)
        {
            Src = src;
            Collision = collision;
        }

        #endregion
    }
}