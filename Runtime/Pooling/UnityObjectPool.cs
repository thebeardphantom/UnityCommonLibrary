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

        public readonly bool ManageActiveState;

        private readonly Stack<GameObject> _pool = new Stack<GameObject>();

        private readonly HashSet<GameObject> _instances = new HashSet<GameObject>();

        #endregion

        #region Properties

        public int Active => _instances.Count;

        public int Pooled => _pool.Count;

        #endregion

        #region Constructors

        public UnityObjectPool(GameObject prefab, bool manageActiveState = false)
        {
            Prefab = prefab;
            ManageActiveState = manageActiveState;
#if !UNITY_EDITOR
            if(ManageActiveState)
            {
                prefab.SetActive(false);
            }
#endif
        }

        #endregion

        #region Methods

        public void ReturnAll()
        {
            foreach (var instance in _instances)
            {
                ReturnInstanceToPool(instance, false);
            }

            _instances.Clear();
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
                ReturnInstanceToPool(instance);
            }
            else
            {
                Debug.LogError($"Returning {instance} to invalid pool");
                Object.Destroy(instance);
            }
        }

        private void ReturnInstanceToPool(GameObject instance, bool removeFromInstancesSet = true)
        {
            if (ManageActiveState)
            {
                instance.gameObject.SetActive(false);
            }

            _pool.Push(instance);
            if (removeFromInstancesSet)
            {
                _instances.Remove(instance);
            }
        }

        private IDisposable GetDeactivateScope(GameObject prefab)
        {
#if UNITY_EDITOR
            return ManageActiveState ? new SetGameObjectActiveScope(prefab, false) : default;
#else
            return null;
#endif
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