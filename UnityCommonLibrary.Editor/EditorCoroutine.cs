using System.Collections;
using UnityEditor;

namespace BeardPhantom.UCL.Editor
{
    public class EditorCoroutine
    {
        #region Fields

        private readonly IEnumerator _routine;

        #endregion

        #region Constructors

        private EditorCoroutine(IEnumerator routine)
        {
            _routine = routine;
        }

        #endregion

        #region Methods

        public static EditorCoroutine Start(IEnumerator routine)
        {
            var coroutine = new EditorCoroutine(routine);
            coroutine.Start();

            return coroutine;
        }

        public void Stop()
        {
            EditorApplication.update -= Update;
        }

        private void Start()
        {
            EditorApplication.update -= Update;
            EditorApplication.update += Update;
        }

        private void Update()
        {
            if (!_routine.MoveNext())
            {
                Stop();
            }
        }

        #endregion
    }
}