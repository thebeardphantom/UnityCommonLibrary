using System;
using System.Collections.Generic;
using BeardPhantom.UCL.Utility;
using UnityEngine.Experimental.LowLevel;
using UnityEngine.Experimental.PlayerLoop;

namespace BeardPhantom.UCL
{
    public static class PlayerLoopHook
    {
        #region Types

        private class FuncEntry : IComparable<FuncEntry>
        {
            #region Fields

            public int Order;
            public PlayerLoopSystem.UpdateFunction Function;

            #endregion

            #region Methods

            /// <inheritdoc />
            public int CompareTo(FuncEntry other)
            {
                return Order.CompareTo(other.Order);
            }

            #endregion
        }

        #endregion

        #region Fields

        private static readonly List<FuncEntry> _callbacks = new List<FuncEntry>();

        #endregion

        #region Methods

        public static void Create()
        {
            var loop = PlayerLoop.GetDefaultPlayerLoop();
            PlayerLoopUtility.InsertUpdateLoopSystem<Update.ScriptRunBehaviourUpdate>(
                ref loop,
                new PlayerLoopSystem
                {
                    updateDelegate = UpdateAll
                });
            PlayerLoop.SetPlayerLoop(loop);
        }

        public static void Teardown()
        {
            PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
        }

        public static void Register(PlayerLoopSystem.UpdateFunction func, int order = 0)
        {
            _callbacks.Add(
                new FuncEntry
                {
                    Function = func,
                    Order = order
                });
            _callbacks.Sort();
        }

        public static void Unregister(PlayerLoopSystem.UpdateFunction func)
        {
            _callbacks.RemoveAll(c => c.Function == func);
        }

        private static void UpdateAll()
        {
            foreach (var callback in _callbacks)
            {
                callback.Function();
            }
        }

        #endregion
    }
}