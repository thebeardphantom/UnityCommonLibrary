using System;
using UnityEngine;

namespace BeardPhantom.UCL
{
    public struct SetGameObjectActiveScope : IDisposable
    {
        #region Fields

        private readonly GameObject _prefab;

        private readonly bool _desiredState;

        private readonly bool _wasActive;

        #endregion

        #region Constructors

        public SetGameObjectActiveScope(GameObject prefab, bool desiredState)
        {
            _prefab = prefab;
            _desiredState = desiredState;
            _wasActive = _prefab.activeSelf;
            if (_prefab.activeSelf != desiredState)
            {
                _prefab.SetActive(_desiredState);
            }
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Dispose()
        {
            if (_wasActive != _desiredState)
            {
                _prefab.SetActive(_wasActive);
            }
        }

        #endregion
    }
}