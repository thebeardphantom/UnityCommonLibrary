using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    /// <summary>
    /// Stores the contents of a 2D trigger.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TriggerContents2D : TriggerContents<Collider2D>
    {
        #region Methods

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();
            if (!Trigger.isTrigger)
            {
                UCLCore.Logger.LogError("", "Collider2D must be a trigger", this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnExit(other);
        }

        #endregion
    }
}