using System;

namespace BeardPhantom.UCL
{
    public class UnlimitedProcessLimit : EventBusProcessLimit
    {
        #region Methods

        /// <inheritdoc />
        public override bool CanContinue(int processedEventCount, TimeSpan timeSpentProcessing)
        {
            return true;
        }

        #endregion
    }
}