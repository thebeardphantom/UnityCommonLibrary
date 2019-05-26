using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class CoroutineUtility
    {
        #region Fields

        private static readonly Dictionary<string, Coroutine> _keyedRoutines =
            new Dictionary<string, Coroutine>();

        private static EmptyMonoBehaviour _surrogate;

        #endregion

        #region Properties

        private static EmptyMonoBehaviour Surrogate
        {
            get
            {
                EnsureSurrogate();

                return _surrogate;
            }
        }

        #endregion

        #region Methods

        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            return Surrogate.StartCoroutine(routine);
        }

        public static Coroutine StartCoroutine(string key, IEnumerator routine)
        {
            var coroutine = Surrogate.StartCoroutine(routine);
            _keyedRoutines.Add(key, coroutine);

            return coroutine;
        }

        public static void StopAllCoroutines()
        {
            Surrogate.StopAllCoroutines();
            _keyedRoutines.Clear();
        }

        public static void StopKeyedCoroutines()
        {
            foreach (var coroutine in _keyedRoutines.Values)
            {
                StopCoroutine(coroutine);
            }

            _keyedRoutines.Clear();
        }

        public static void StopCoroutine(IEnumerator routine)
        {
            Surrogate.StopCoroutine(routine);
        }

        public static void StopCoroutine(Coroutine routine)
        {
            Surrogate.StopCoroutine(routine);
        }

        public static void StopCoroutine(string key)
        {
            Coroutine routine;

            if (_keyedRoutines.TryGetValue(key, out routine))
            {
                Surrogate.StopCoroutine(routine);
            }
        }

        private static void EnsureSurrogate()
        {
            if (!_surrogate)
            {
                _surrogate =
                    ComponentUtility.Create<EmptyMonoBehaviour>("CoroutineUtilitySurrogate");

                _surrogate.hideFlags = HideFlags.NotEditable;
                Object.DontDestroyOnLoad(_surrogate);
            }
        }

        #endregion
    }
}