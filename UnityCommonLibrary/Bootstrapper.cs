using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEngine;

#endif

namespace UnityCommonLibrary
{
    public abstract class Bootstrapper : MonoBehaviour
    {
        #region Types

        public class BootstrapTaskHandle
        {
            #region Fields

            public bool IsDone;

            #endregion
        }

        public enum BootstrapperState
        {
            WaitingOnTasks,
            Complete
        }

        #endregion

        #region Fields

        private const string BOOTSTRAPPER_LOADED_SCENES = "BOOTSTRAPPER_LOADED_SCENES";

        private readonly Queue<BootstrapTaskHandle> _tasks = new Queue<BootstrapTaskHandle>();

        [Min(0)]
        [SerializeField]
        private int _bootstrapSceneBuildIndex;

        #endregion

        #region Properties

        public static Bootstrapper Instance { get; private set; }

        public BootstrapperState State { get; private set; } = BootstrapperState.Complete;

        #endregion

        #region Methods

        protected abstract void GetBootstrapTasks(Queue<BootstrapTaskHandle> tasks);

        public void BeginBootstrapping()
        {
            if (State == BootstrapperState.Complete)
            {
                State = BootstrapperState.WaitingOnTasks;
                _tasks.Clear();
                GetBootstrapTasks(_tasks);
            }
        }

        protected virtual void Awake()
        {
            if (EnsureInstance())
            {
                StoreDesiredScenes();
                SceneManager.LoadScene(_bootstrapSceneBuildIndex);
            }
        }

        protected virtual void Start()
        {
            if (EnsureInstance())
            {
                BeginBootstrapping();
            }
        }

        protected virtual void Update()
        {
            if (State == BootstrapperState.Complete)
            {
                return;
            }

            while (_tasks.Count > 0)
            {
                if (_tasks.Peek().IsDone)
                {
                    _tasks.Dequeue();
                }
                else
                {
                    break;
                }
            }

            if (_tasks.Count == 0)
            {
                State = BootstrapperState.Complete;
                RestoreDesiredScenes();
            }
        }

        private bool EnsureInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return false;
            }

            Instance = this;
            DontDestroyOnLoad(this);
            return true;
        }

        private void StoreDesiredScenes()
        {
#if UNITY_EDITOR
            var scenePaths = new List<string>();
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex != _bootstrapSceneBuildIndex)
                {
                    scenePaths.Add(scene.path);
                }
            }

            var scenesString = string.Join("\n", scenePaths);
            SessionState.SetString(BOOTSTRAPPER_LOADED_SCENES, scenesString);
#endif
        }

        private void RestoreDesiredScenes()
        {
#if UNITY_EDITOR
            var desiredScenesStr = SessionState.GetString(BOOTSTRAPPER_LOADED_SCENES, null);
            if (desiredScenesStr == null)
            {
                SceneManager.LoadScene(_bootstrapSceneBuildIndex + 1);
            }
            else
            {
                var desiredScenePaths = desiredScenesStr.Split('\n');
                for (var i = 0; i < desiredScenePaths.Length; i++)
                {
                    var path = desiredScenePaths[i];
                    EditorSceneManager.LoadSceneInPlayMode(
                        path,
                        new LoadSceneParameters(i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive));
                }
            }
#else
            SceneManager.LoadScene(_bootstrapSceneBuildIndex + 1);
#endif
        }

        #endregion
    }
}