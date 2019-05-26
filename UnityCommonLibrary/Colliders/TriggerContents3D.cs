using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    /// <summary>
    /// Stores the contents of a trigger.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerContents3D : TriggerContents<Collider>
    {
        #region Methods

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();
            if (!Trigger.isTrigger)
            {
                UCLCore.Logger.LogError("", "Collider must be a trigger", this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit(other);
        }

        #endregion
    }
}