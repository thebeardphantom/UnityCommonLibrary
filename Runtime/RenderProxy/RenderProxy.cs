using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace BeardPhantom.UCL
{
    public class RenderProxy : IDisposable
    {
        #region Fields

        private static readonly List<Renderer> _renderers = new List<Renderer>();

        private static readonly int _defaultLayer = LayerMask.NameToLayer("Default");

        public readonly ReadOnlyCollection<RenderProxySubObject> SubObjects;

        private readonly List<RenderProxySubObject> _subObjects = new List<RenderProxySubObject>();

        #endregion

        #region Constructors

        public RenderProxy(GameObject root, RenderProxyOptions options = (RenderProxyOptions) (-1))
        {
            var logUnsupportedTypes = options.HasFlagFast(RenderProxyOptions.LogUnsupportedRendererTypes);
            SubObjects = new ReadOnlyCollection<RenderProxySubObject>(_subObjects);
            try
            {
                _renderers.Clear();
                root.GetComponentsInChildren(options.HasFlagFast(RenderProxyOptions.FindInactiveRenderers), _renderers);
                Assert.IsTrue(_renderers.Count > 0, "_renderers.Count > 0");
                foreach (var renderer in _renderers)
                {
                    switch (renderer)
                    {
                        case MeshRenderer meshRenderer:
                        {
                            _subObjects.Add(new MeshRendererSubObject(meshRenderer, options));
                            break;
                        }
                        default:
                        {
                            if (logUnsupportedTypes)
                            {
                                UCLCore.Logger.LogWarning(
                                    nameof(RenderProxy),
                                    $"Unsupported renderer type: {renderer.GetType()}",
                                    root);
                            }

                            break;
                        }
                    }
                }
            }
            finally
            {
                _renderers.Clear();
            }
        }

        #endregion

        #region Methods

        public void Render(
            Vector3 position = default,
            Quaternion rotation = default,
            Vector3 localScale = default,
            int layer = -1,
            Camera camera = null)
        {
            var trs = Matrix4x4.TRS(position, rotation, localScale);
            Render(trs, layer, camera);
        }

        public void Render(Matrix4x4 transformation, int layer = -1, Camera camera = null)
        {
            layer = layer < 0 ? _defaultLayer : layer;
            for (var i = 0; i < _subObjects.Count; i++)
            {
                var obj = _subObjects[i];
                obj.Render(transformation, layer, camera);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            for (var i = 0; i < _subObjects.Count; i++)
            {
                _subObjects[i].Dispose();
            }

            _subObjects.Clear();
        }

        #endregion
    }
}