using System;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.Tilemaps;

namespace BeardPhantom.UCL.Utility
{
    public static class GameObjectUtility
    {
        #region Methods

        public static void ForceEnable(this GameObject obj)
        {
            if (obj.activeInHierarchy)
            {
                return;
            }

            var target = obj.transform;

            do
            {
                target.gameObject.SetActive(true);
                target = target.parent;
            }
            while (target != null);
        }

        public static bool MoveToGround(
            this GameObject obj,
            LayerMask? mask = null)
        {
            Bounds? bounds = null;
            var colliders = obj.GetComponentsInChildren<Collider>(true);

            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].isTrigger)
                {
                    continue;
                }

                if (!bounds.HasValue)
                {
                    bounds = colliders[i].bounds;
                }
                else
                {
                    bounds.Value.Encapsulate(colliders[i].bounds);
                }
            }

            if (!bounds.HasValue)
            {
                var renderers = obj.GetComponentsInChildren<Renderer>(true);

                for (var i = 0; i < renderers.Length; i++)
                {
                    if (!bounds.HasValue)
                    {
                        bounds = renderers[i].bounds;
                    }
                    else
                    {
                        bounds.Value.Encapsulate(renderers[i].bounds);
                    }
                }
            }

            if (bounds.HasValue)
            {
                mask = mask ?? Physics.DefaultRaycastLayers;

                var allHits =
                    Physics.RaycastAll(
                        obj.transform.position + Vector3.up * 0.1f,
                        Vector3.down,
                        1000f,
                        mask.Value);

                Array.Sort(
                    allHits,
                    (r1, r2) => r1.distance.CompareTo(r2.distance));

                for (var i = 0; i < allHits.Length; i++)
                {
                    if (!allHits[i].transform.IsChildOf(obj.transform))
                    {
                        obj.transform.position =
                            allHits[i].point + Vector3.up * (bounds.Value.extents.y - 0.1f);

                        return true;
                    }
                }
            }

            return false;
        }

        public static T AddOrGetComponent<T>(this GameObject obj)
            where T : Component
        {
            var component = obj.GetComponent<T>();

            if (component == null)
            {
                return obj.AddComponent<T>();
            }

            return component;
        }

        public static void SetLayerRecursive(this GameObject obj, int layer)
        {
            obj.layer = layer;

            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetLayerRecursive(layer);
            }
        }

        public static string GetPath(GameObject obj)
        {
            var sb = new StringBuilder("/" + obj.name);

            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                sb.Insert(0, "/" + obj.name);
            }

            return sb.ToString();
        }

        public static void Toggle(bool enabled, params GameObject[] gameObjects)
        {
            foreach (var go in gameObjects)
            {
                if (go != null)
                {
                    go.SetActive(enabled);
                }
            }
        }

        public static Bounds GetCollectiveBounds(this GameObject root)
        {
            var children = root.GetComponentsInChildren<Transform>(true);
            var bounds = new Bounds();
            foreach (var transform in children)
            {
                bounds.Encapsulate(GetLocalBounds(transform.gameObject));
            }

            return bounds;
        }

        public static Bounds GetLocalBounds(this GameObject gameObject)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform)
            {
                return new Bounds(rectTransform.rect.center, rectTransform.rect.size);
            }

            var renderer = gameObject.GetComponent<Renderer>();
            if (renderer is MeshRenderer)
            {
                var filter = renderer.GetComponent<MeshFilter>();
                if (filter != null && filter.sharedMesh != null)
                {
                    return filter.sharedMesh.bounds;
                }
            }

            if (renderer is SpriteRenderer spriteRenderer)
            {
                return spriteRenderer.bounds;
            }

            if (renderer is SpriteMask mask)
            {
                return mask.bounds;
            }

            if (renderer is SpriteShapeRenderer shapeRenderer)
            {
                return shapeRenderer.bounds;
            }

            if (renderer is TilemapRenderer)
            {
                var tilemap = renderer.GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    return tilemap.localBounds;
                }
            }

            return new Bounds(Vector3.zero, Vector3.zero);
        }

        #endregion
    }
}