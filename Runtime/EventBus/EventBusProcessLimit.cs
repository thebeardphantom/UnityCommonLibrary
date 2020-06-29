using System;

namespace BeardPhantom.UCL
{
    [Serializable]
    public abstract class EventBusProcessLimit
    {
        #region Methods

        public abstract bool CanContinue(int processedEventCount, TimeSpan timeSpentProcessing);

        #endregion
    }
}