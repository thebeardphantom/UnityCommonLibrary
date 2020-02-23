using System;
using UnityEngine;

namespace BeardPhantom.UCL
{
    public abstract class RenderProxySubObject : IDisposable
    {
        #region Fields

        public Matrix4x4 LocalToWorld;

        #endregion

        #region Methods

        public abstract void Render(Matrix4x4 transformation, int layer, Camera camera);

        /// <inheritdoc />
        public abstract void Dispose();

        #endregion
    }
}