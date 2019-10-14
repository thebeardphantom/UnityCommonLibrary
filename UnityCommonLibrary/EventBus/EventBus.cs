using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public class EventBus
    {
        #region Fields

        private readonly Dictionary<Type, IListenerCollection> _listenerCollections
            = new Dictionary<Type, IListenerCollection>();

        private readonly Queue<EventBusEventData> _readyEvents
            = new Queue<EventBusEventData>();

        private readonly List<GatedEventBusEventData> _pendingGatedEvents
            = new List<GatedEventBusEventData>();

        #endregion

        #region Methods

        public void PostEvent<T>() where T : EventBusEventData, new()
        {
            PostEvent(new T());
        }

        public void PostEvent<T>(T evtData) where T : EventBusEventData
        {
            if (evtData == null)
            {
                UCLCore.Logger.LogError("", "Null event posted");
                return;
            }

            if (evtData is GatedEventBusEventData gatedEvent)
            {
                _pendingGatedEvents.Add(gatedEvent);
            }
            else
            {
                _readyEvents.Enqueue(evtData);
            }
        }

        public void RegisterObserver<T>(
            EventPosted<T> callback,
            Predicate<T> predicate = null,
            bool once = false)
            where T : EventBusEventData
        {
            var collection = GetCollection<T>();
            if (collection == null)
            {
                collection = new ListenerCollection<T>();
                _listenerCollections.Add(typeof(T), collection);
            }

            collection.Add(new EventBusObserver<T>(callback, predicate, once));
        }

        public void RemoveObserver<T>(EventPosted<T> observer) where T : EventBusEventData
        {
            var collection = GetCollection<T>();
            collection.Remove(observer);
        }

        public void RemoveObserver(object target)
        {
            foreach (var collection in _listenerCollections.Values)
            {
                collection.RemoveTarget(target);
            }
        }

        public void Process()
        {
            while (_readyEvents.Count > 0)
            {
                var evt = _readyEvents.Dequeue();
                if (_listenerCollections.TryGetValue(evt.GetType(), out var collection))
                {
                    collection.Publish(evt);
                }
            }

            for (var i = _pendingGatedEvents.Count - 1; i >= 0; i--)
            {
                var evt = _pendingGatedEvents[i];
                if (evt.CheckReady())
                {
                    _readyEvents.Enqueue(evt);
                }
            }
        }

        private ListenerCollection<T> GetCollection<T>() where T : EventBusEventData
        {
            if (_listenerCollections.TryGetValue(typeof(T), out var genericCollection))
            {
                return (ListenerCollection<T>) genericCollection;
            }

            return null;
        }

        #endregion
    }
}