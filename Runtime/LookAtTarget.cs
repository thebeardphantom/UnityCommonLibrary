using UnityEngine;
using UnityEngine.Serialization;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Utility behaviour for looking at a given transform every frame.
    /// </summary>
    [ExecuteInEditMode]
    public class LookAtTarget : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Target to look at.
        /// </summary>
        [SerializeField]
        [FormerlySerializedAs("_target")]
        public Transform Target;

        /// <summary>
        /// Scaler for final rotation after looking at target.
        /// </summary>
        [SerializeField]
        private Vector3 _amount = Vector3.one;

        #endregion

        #region Methods

        private void LateUpdate()
        {
            if (Target == null)
            {
                return;
            }

            transform.LookAt(Target);

            transform.eulerAngles = Vector3.Scale(
                transform.eulerAngles,
                _amount);
        }

        #endregion
    }
}