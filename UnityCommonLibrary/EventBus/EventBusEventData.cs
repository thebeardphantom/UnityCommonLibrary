using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public abstract class EventBusEventData
    {
        #region Fields

        public EventBusEventData Source;

        #endregion

        #region Methods

        public EventBusEventData GetOrigin()
        {
            var origin = Source;
            while (origin != null)
            {
                if (origin.Source == null)
                {
                    break;
                }

                origin = origin.Source;
            }

            return origin;
        }

        public T GetUpstreamEvent<T>() where T : EventBusEventData
        {
            var current = Source;
            while (current != null)
            {
                if (current is T currentT)
                {
                    return currentT;
                }

                current = current.Source;
            }

            return null;
        }

        public List<EventBusEventData> GetEventPath()
        {
            var list = new List<EventBusEventData>();
            GetEventPath(list);
            return list;
        }

        public void GetEventPath(List<EventBusEventData> list)
        {
            var lastVisited = this;
            while (lastVisited != null)
            {
                list.Add(lastVisited);
                lastVisited = lastVisited.Source;
            }

            list.Reverse();
        }

        #endregion
    }
}