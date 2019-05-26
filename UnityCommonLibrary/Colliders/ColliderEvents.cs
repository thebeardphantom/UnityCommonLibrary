using BeardPhantom.UCL.Signals;
using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    /// <summary>
    /// A behaviour for responding to collision and trigger events
    /// outside of the GameObject that stores the Collider component.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ColliderEvents : MonoBehaviour
    {
        #region Fields

        public readonly Signal<ColliderEvents, Collision> CollisionEnter
            = new Signal<ColliderEvents, Collision>();

        public readonly Signal<ColliderEvents, Collision> CollisionExit
            = new Signal<ColliderEvents, Collision>();

        public readonly Signal<ColliderEvents, Collision> CollisionStay
            = new Signal<ColliderEvents, Collision>();

        public readonly Signal<ColliderEvents, Collider> TriggerEnter
            = new Signal<ColliderEvents, Collider>();

        public readonly Signal<ColliderEvents, Collider> TriggerExit
            = new Signal<ColliderEvents, Collider>();

        public readonly Signal<ColliderEvents, Collider> TriggerStay
            = new Signal<ColliderEvents, Collider>();

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
            CollisionEnter.Publish(this, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExit.Publish(this, collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            CollisionStay.Publish(this, collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter.Publish(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit.Publish(this, other);
        }

        private void OnTriggerStay(Collider other)
        {
            TriggerStay.Publish(this, other);
        }

        #endregion
    }
}