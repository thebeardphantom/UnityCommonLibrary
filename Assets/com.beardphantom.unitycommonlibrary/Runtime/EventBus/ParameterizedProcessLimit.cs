using System;

namespace BeardPhantom.UCL
{
    [Serializable]
    public class ParameterizedProcessLimit : EventBusProcessLimit
    {
        #region Fields

        public int MaxEventCount;

        public double MaxProcessingTimeMs;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override bool CanContinue(int processedEventCount, TimeSpan timeSpentProcessing)
        {
            return (MaxEventCount <= 0 || processedEventCount <= MaxEventCount)
                && timeSpentProcessing.TotalMilliseconds <= MaxProcessingTimeMs;
        }

        #endregion
    }
}