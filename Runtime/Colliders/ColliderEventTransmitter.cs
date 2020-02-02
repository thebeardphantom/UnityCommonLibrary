using BeardPhantom.UCL.Signals;
using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    /// <summary>
    /// A behaviour for responding to collision and trigger events
    /// outside of the GameObject that stores the Collider component.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ColliderEventTransmitter : MonoBehaviour
    {
        #region Fields

        public readonly Signal<CollisionEvent> CollisionEnter
            = new Signal<CollisionEvent>();

        public readonly Signal<CollisionEvent> CollisionExit
            = new Signal<CollisionEvent>();

        public readonly Signal<CollisionEvent> CollisionStay
            = new Signal<CollisionEvent>();

        public readonly Signal<TriggerEvent> TriggerEnter
            = new Signal<TriggerEvent>();

        public readonly Signal<TriggerEvent> TriggerExit
            = new Signal<TriggerEvent>();

        public readonly Signal<TriggerEvent> TriggerStay
            = new Signal<TriggerEvent>();

        #endregion

        #region Properties

        public Collider EventCollider { get; private set; }

        #endregion

        #region Methods

        private void Awake()
        {
            EventCollider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnter.Publish(new CollisionEvent(this, collision));
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExit.Publish(new CollisionEvent(this, collision));
        }

        private void OnCollisionStay(Collision collision)
        {
            CollisionStay.Publish(new CollisionEvent(this, collision));
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter.Publish(new TriggerEvent(this, other));
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit.Publish(new TriggerEvent(this, other));
        }

        private void OnTriggerStay(Collider other)
        {
            TriggerStay.Publish(new TriggerEvent(this, other));
        }

        #endregion
    }
}