using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Utility component to DontDestroyOnLoad an object on Awake
    /// </summary>
    [DisallowMultipleComponent]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        #region Methods

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        #endregion
    }
}