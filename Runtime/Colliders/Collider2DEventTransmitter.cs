using BeardPhantom.UCL.Signals;
using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    /// <summary>
    /// A behaviour for responding to collision and trigger events
    /// outside of the GameObject that stores the Collider2D component.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Collider2DEventTransmitter : MonoBehaviour
    {
        #region Fields

        public readonly Signal<Collision2DEvent> CollisionEnter2D
            = new Signal<Collision2DEvent>();

        public readonly Signal<Collision2DEvent> CollisionExit2D
            = new Signal<Collision2DEvent>();

        public readonly Signal<Collision2DEvent> CollisionStay2D
            = new Signal<Collision2DEvent>();

        public readonly Signal<Trigger2DEvent> TriggerEnter2D
            = new Signal<Trigger2DEvent>();

        public readonly Signal<Trigger2DEvent> TriggerExit2D
            = new Signal<Trigger2DEvent>();

        public readonly Signal<Trigger2DEvent> TriggerStay2D
            = new Signal<Trigger2DEvent>();

        #endregion

        #region Properties

        public Collider2D EventCollider { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            EventCollider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter2D.Publish(new Collision2DEvent(this, collision));
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExit2D.Publish(new Collision2DEvent(this, collision));
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CollisionStay2D.Publish(new Collision2DEvent(this, collision));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter2D.Publish(new Trigger2DEvent(this, other));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit2D.Publish(new Trigger2DEvent(this, other));
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStay2D.Publish(new Trigger2DEvent(this, other));
        }

        #endregion
    }
}