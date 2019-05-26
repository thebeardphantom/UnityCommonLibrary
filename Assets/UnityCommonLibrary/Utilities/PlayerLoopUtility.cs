using System.Linq;
using UnityEngine.Experimental.LowLevel;

namespace BeardPhantom.UCL.Utility
{
    /// <summary>
    /// Class for making it easier to work with UnityEngine.Experimental.LowLevel classes.
    /// </summary>
    public static class PlayerLoopUtility
    {
        #region Methods

        /// <summary>
        /// Inserts PlayerLoopSystem newSystem at the location of preexisting system of type T plus offset
        /// </summary>
        public static void InsertUpdateLoopSystem<T>(
            ref PlayerLoopSystem parentSystem,
            PlayerLoopSystem newSystem,
            int offset = 1)
        {
            if (parentSystem.subSystemList == null)
            {
                return;
            }

            var subSystems = parentSystem.subSystemList.ToList();
            for (var i = 0; i < subSystems.Count; i++)
            {
                var system = subSystems[i];
                if (system.type == typeof(T))
                {
                    subSystems.Insert(i + offset, newSystem);
                    break;
                }

                if (system.subSystemList != null)
                {
                    InsertUpdateLoopSystem<T>(ref system, newSystem, offset);
                    subSystems[i] = system;
                }
            }

            parentSystem.subSystemList = subSystems.ToArray();
        }

        #endregion
    }
}