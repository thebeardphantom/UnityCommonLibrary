using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.UCL.Pooling
{
    public class UnityObjectPool
    {
        #region Fields

        public readonly GameObject Prefab;

        private readonly Stack<GameObject> _pool = new Stack<GameObject>();

        private readonly HashSet<GameObject> _instances = new HashSet<GameObject>();

        #endregion

        #region Properties

        public int Active => _instances.Count;

        public int Pooled => _pool.Count;

        #endregion

        #region Constructors

        public UnityObjectPool(GameObject prefab)
        {
            Prefab = prefab;
#if !UNITY_EDITOR
            prefab.SetActive(false);
#endif
        }

        #endregion

        #region Methods

        private static IDisposable GetDeactivateScope(GameObject prefab)
        {
#if UNITY_EDITOR
            return new SetGameObjectActiveScope(prefab, false);
#else
            return null;
#endif
        }

        public void EnsurePoolCount(int count, Transform parent = null)
        {
            if (_pool.Count >= count)
            {
                return;
            }

            using (GetDeactivateScope(Prefab))
            {
                for (var i = 0; i < count; i++)
                {
                    var instance = GetNew(parent);
                    _pool.Push(instance);
                }
            }
        }

        public T Retrieve<T>(
            Transform parent = null,
            Vector3 position = default,
            Quaternion rotation = default) where T : Component
        {
            var gObj = Retrieve(parent, position, rotation);
            return gObj.GetComponent<T>();
        }

        public GameObject Retrieve(
            Transform parent = null,
            Vector3 position = default,
            Quaternion rotation = default)
        {
            GameObject instance;
            if (_pool.Count == 0)
            {
                using (GetDeactivateScope(Prefab))
                {
                    instance = GetNew(parent, position, rotation);
                }
            }
            else
            {
                instance = _pool.Pop();
                instance.transform.SetPositionAndRotation(position, rotation);
            }

            var obj = instance;
            _instances.Add(obj);
            return obj;
        }

        public void Return(GameObject instance)
        {
            if (_pool.Contains(instance))
            {
                return;
            }

            if (_instances.Contains(instance))
            {
                instance.gameObject.SetActive(false);
                _instances.Remove(instance);
                _pool.Push(instance);
            }
            else
            {
                Debug.LogError($"Returning {instance} to invalid pool");
                Object.Destroy(instance);
            }
        }

        private GameObject GetNew(
            Transform parent = null,
            Vector3 position = default,
            Quaternion rotation = default)
        {
            return UCLCore.Instantiate(Prefab, position, rotation, parent);
        }

        #endregion
    }
}