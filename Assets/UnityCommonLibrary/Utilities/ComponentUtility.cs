using System;
using System.Collections.Generic;
using BeardPhantom.UCL.Pooling;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeardPhantom.UCL.Utility
{
    public static class ComponentUtility
    {
        #region Methods

        public static T Create<T>() where T : Component
        {
            return Create<T>(typeof(T).Name);
        }

        public static T Create<T>(string name) where T : Component
        {
            return (T) Create(typeof(T), name);
        }

        public static Component Create(Type t)
        {
            return Create(t, t.Name);
        }

        public static Component Create(Type t, string name)
        {
            return new GameObject(name).AddComponent(t);
        }

        public static void FindAll<T>(List<T> list, bool includeInactive = true) where T : Component
        {
            using (var sceneRoots = ListPool<GameObject>.Obtain())
            {
                using (var appendList = ListPool<T>.Obtain())
                {
                    for (var i = 0; i < SceneManager.sceneCount; i++)
                    {
                        var scene = SceneManager.GetSceneAt(i);
                        sceneRoots.Collection.Clear();
                        scene.GetRootGameObjects(sceneRoots);
                        foreach (var root in sceneRoots)
                        {
                            appendList.Collection.Clear();
                            root.GetComponentsInChildren(includeInactive, appendList.Collection);
                            list.AddRange(appendList);
                        }
                    }
                }
            }
        }

        public static IList<T> FindAll<T>(bool includeInactive = true) where T : Component
        {
            var list = new List<T>();
            FindAll(list, includeInactive);
            return list;
        }

        public static void SetEnabledAll(
            bool enabled,
            params Behaviour[] behaviors)
        {
            foreach (var b in behaviors)
            {
                if (b != null)
                {
                    b.enabled = enabled;
                }
            }
        }

        #endregion
    }
}