using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public abstract class EventBusEventData { }

    public abstract class GatedEventBusEventData : EventBusEventData
    {
        #region Fields

        private readonly List<IPendingEventBarrier> _barriers = new List<IPendingEventBarrier>();

        #endregion

        #region Methods

        public void AddBarrier(IPendingEventBarrier barrier)
        {
            _barriers.Add(barrier);
        }

        internal bool CheckReady()
        {
            for (var i = 0; i < _barriers.Count; i++)
            {
                if (!_barriers[i].Complete)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}