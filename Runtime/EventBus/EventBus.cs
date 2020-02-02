using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public class EventBus
    {
        #region Fields

        private readonly Dictionary<Type, IListenerCollection> _listenerCollections
            = new Dictionary<Type, IListenerCollection>();

        private readonly Queue<EventBusEvent> _readyEvents
            = new Queue<EventBusEvent>();

        #endregion

        #region Methods

        public void PostEvent<T>() where T : EventBusEvent, new()
        {
            PostEvent(new T());
        }

        public void PostEvent<T>(T evtData) where T : EventBusEvent
        {
            if (evtData == null)
            {
                UCLCore.Logger.LogError("", "Null event posted");
                return;
            }

            _readyEvents.Enqueue(evtData);
        }

        public void RegisterObserver<T>(
            EventPosted<T> callback,
            Predicate<T> predicate = null,
            bool once = false)
            where T : EventBusEvent
        {
            var collection = GetCollection<T>();
            if (collection == null)
            {
                collection = new ListenerCollection<T>();
                _listenerCollections.Add(typeof(T), collection);
            }

            collection.Add(new EventBusObserver<T>(callback, predicate, once));
        }

        public void RemoveObserver<T>(EventPosted<T> observer) where T : EventBusEvent
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

        public void PumpEvents()
        {
            while (_readyEvents.Count > 0)
            {
                var evt = _readyEvents.Dequeue();
                if (_listenerCollections.TryGetValue(evt.GetType(), out var collection))
                {
                    collection.Publish(evt);
                }
            }
        }

        private ListenerCollection<T> GetCollection<T>() where T : EventBusEvent
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