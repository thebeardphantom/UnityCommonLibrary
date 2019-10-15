using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public abstract class EventBusEvent { }

    public abstract class GatedEventBusEvent : EventBusEvent
    {
        #region Fields

        private readonly List<IGatedEventBarrier> _barriers = new List<IGatedEventBarrier>();

        #endregion

        #region Methods

        public void AddBarrier(IGatedEventBarrier barrier)
        {
            _barriers.Add(barrier);
        }

        internal bool CheckReady()
        {
            if (_barriers.Count == 0)
            {
                return false;
            }

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