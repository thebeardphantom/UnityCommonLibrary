using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UCL.Pooling
{
    public class UnityObjectPool
    {
        #region Fields

        public readonly GameObject Prefab;

        private readonly Queue<GameObject> _pool = new Queue<GameObject>();

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
        }

        #endregion

        #region Methods

        private static void SetTransformValues(
            Transform transform,
            Vector3 position,
            Quaternion rotation,
            bool localPosition,
            bool localRotation)
        {
            if (!localPosition && !localRotation)
            {
                transform.SetPositionAndRotation(position, rotation);
            }
            else
            {
                if (localPosition)
                {
                    transform.localPosition = position;
                }
                else
                {
                    transform.position = position;
                }

                if (localRotation)
                {
                    transform.localRotation = rotation;
                }
                else
                {
                    transform.rotation = rotation;
                }
            }
        }

        public T Retrieve<T>(
            Transform parent = null,
            Vector3? position = default,
            Quaternion? rotation = null,
            bool localPosition = true,
            bool localRotation = true) where T : Component
        {
            var gObj = Retrieve(parent, position, rotation, localPosition, localRotation);
            return gObj.GetComponent<T>();
        }

        public GameObject Retrieve(
            Transform parent = null,
            Vector3? position = default,
            Quaternion? rotation = null,
            bool localPosition = true,
            bool localRotation = true)
        {
            var obj = RetrieveInternal(parent, position, rotation, localPosition, localRotation);
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
                _pool.Enqueue(instance);
            }
            else
            {
                Debug.LogError($"Returning {instance} to invalid pool");
                Object.Destroy(instance);
            }
        }

        private GameObject RetrieveInternal(
            Transform parent,
            Vector3? position,
            Quaternion? rotation,
            bool localPosition,
            bool localRotation)
        {
            rotation = rotation ?? Prefab.transform.rotation;
            position = position ?? Prefab.transform.position;

            GameObject instance;
            if (_pool.Count == 0)
            {
                if (!localPosition && !localRotation)
                {
                    return UCLCore.Instantiate(Prefab, position.Value, rotation.Value, parent);
                }

                instance = UCLCore.Instantiate(Prefab, parent);
            }
            else
            {
                instance = _pool.Dequeue();
            }

            SetTransformValues(
                instance.transform,
                position.Value,
                rotation.Value,
                localPosition,
                localRotation);

            return instance;
        }

        #endregion
    }
}