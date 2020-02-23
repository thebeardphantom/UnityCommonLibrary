using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UCL
{
    public class MeshRendererSubObject : RenderProxySubObject
    {
        #region Fields

        private static readonly List<Material> _sharedMaterialsBuildList = new List<Material>();

        public readonly Mesh Mesh;

        public readonly Material[] Materials;

        public readonly int SubMeshCount;

        private readonly RenderProxyOptions _options;

        #endregion

        #region Constructors

        public MeshRendererSubObject(MeshRenderer renderer, RenderProxyOptions options)
        {
            _options = options;

            var meshFilter = renderer.GetComponent<MeshFilter>();

            LocalToWorld = renderer.transform.localToWorldMatrix;

            Mesh = options.HasFlagFast(RenderProxyOptions.UseUniqueMeshInstance)
                ? Object.Instantiate(meshFilter.sharedMesh)
                : meshFilter.sharedMesh;

            try
            {
                // Copy shared materials
                _sharedMaterialsBuildList.Clear();
                renderer.GetSharedMaterials(_sharedMaterialsBuildList);
                Materials = new Material[_sharedMaterialsBuildList.Count];
                var useUniqueMaterials = options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstance);
                for (var i = 0; i < _sharedMaterialsBuildList.Count; i++)
                {
                    Materials[i] = useUniqueMaterials
                        ? Object.Instantiate(_sharedMaterialsBuildList[i])
                        : _sharedMaterialsBuildList[i];
                }
            }
            finally
            {
                _sharedMaterialsBuildList.Clear();
            }

            SubMeshCount = Mesh.subMeshCount;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Render(Matrix4x4 transformation, int layer, Camera camera)
        {
            var finalTransformation = transformation * LocalToWorld;
            for (var i = 0; i < SubMeshCount; i++)
            {
                Graphics.DrawMesh(Mesh, finalTransformation, Materials[i], layer, camera, i);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMaterialInstance))
            {
                for (var i = 0; i < Materials.Length; i++)
                {
                    Object.Destroy(Materials[i]);
                }
            }

            if (_options.HasFlagFast(RenderProxyOptions.UseUniqueMeshInstance))
            {
                Object.Destroy(Mesh);
            }
        }

        #endregion
    }
}