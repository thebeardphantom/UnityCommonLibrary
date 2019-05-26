using System.Collections.Generic;

namespace BeardPhantom.UCL.Signals
{
    public static class SignalUtility
    {
        #region Fields

        internal static readonly HashSet<ISignal> AllMessages =
            new HashSet<ISignal>();

        #endregion

        #region Methods

        public static void UnsubscribeFromAll(object target)
        {
            foreach (var s in AllMessages)
            {
                s.UnsubscribeTarget(target);
            }
        }

        #endregion
    }
}