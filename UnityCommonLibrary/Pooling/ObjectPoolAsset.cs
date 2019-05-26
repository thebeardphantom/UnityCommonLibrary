using BeardPhantom.UCL.Attributes;
using UnityEngine;

namespace BeardPhantom.UCL.Pooling
{
    [CustomAssetCreateMenu]
    public class ObjectPoolAsset : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private GameObject _prefab;

        private UnityObjectPool _pool;

        #endregion

        #region Properties

        public UnityObjectPool Pool
        {
            get
            {
                if (!Application.isPlaying)
                {
                    return null;
                }

                return _pool ?? (_pool = new UnityObjectPool(_prefab));
            }
        }

        #endregion
    }
}