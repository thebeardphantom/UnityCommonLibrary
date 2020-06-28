using UnityEngine;

namespace BeardPhantom.UCL
{
    public class Mover : MonoBehaviour
    {
        #region Fields

        public Vector3 Movement;

        public Space MovementSpace;

        public Vector3 Rotation;

        public Space RotationSpace;

        public bool UseRigidbody;

        private Rigidbody _rigidbody;

        #endregion

        #region Methods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (UseRigidbody && _rigidbody != null)
            {
                if (MovementSpace == Space.Self)
                {
                    _rigidbody.velocity = transform.TransformVector(Movement);

                    _rigidbody.angularVelocity =
                        transform.TransformVector(Rotation);
                }
                else
                {
                    _rigidbody.velocity = Movement;
                    _rigidbody.angularVelocity = Rotation;
                }
            }
            else
            {
                transform.Translate(Movement * UnityEngine.Time.deltaTime, MovementSpace);
                transform.Rotate(Rotation * UnityEngine.Time.deltaTime, RotationSpace);
            }
        }

        #endregion
    }
}