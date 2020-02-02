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

        [SerializeField]
        private int _initCapacity;

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

                if (_pool == null)
                {
                    _pool = new UnityObjectPool(_prefab);
                    _pool.EnsurePoolCount(_initCapacity);
                }

                return _pool;
            }
        }

        #endregion
    }
}