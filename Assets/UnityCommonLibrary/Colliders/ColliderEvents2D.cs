using BeardPhantom.UCL.Signals;
using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    /// <summary>
    /// A behaviour for responding to collision and trigger events
    /// outside of the GameObject that stores the Collider2D component.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ColliderEvents2D : MonoBehaviour
    {
        #region Fields

        public readonly Signal<ColliderEvents2D, Collision2D> CollisionEnter2D
            = new Signal<ColliderEvents2D, Collision2D>();

        public readonly Signal<ColliderEvents2D, Collision2D> CollisionExit2D
            = new Signal<ColliderEvents2D, Collision2D>();

        public readonly Signal<ColliderEvents2D, Collision2D> CollisionStay2D
            = new Signal<ColliderEvents2D, Collision2D>();

        public readonly Signal<ColliderEvents2D, Collider2D> TriggerEnter2D
            = new Signal<ColliderEvents2D, Collider2D>();

        public readonly Signal<ColliderEvents2D, Collider2D> TriggerExit2D
            = new Signal<ColliderEvents2D, Collider2D>();

        public readonly Signal<ColliderEvents2D, Collider2D> TriggerStay2D
            = new Signal<ColliderEvents2D, Collider2D>();

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
            CollisionEnter2D.Publish(this, collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExit2D.Publish(this, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CollisionStay2D.Publish(this, collision);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter2D.Publish(this, other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit2D.Publish(this, other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStay2D.Publish(this, other);
        }

        #endregion
    }
}