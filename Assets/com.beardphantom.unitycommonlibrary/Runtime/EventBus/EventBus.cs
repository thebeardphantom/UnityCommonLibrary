using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public class EventBus
    {
        #region Fields

        private readonly Dictionary<Type, IListenerCollection> _listenerCollections
            = new Dictionary<Type, IListenerCollection>();

        private readonly Queue<object> _postedEvents = new Queue<object>();

        #endregion

        #region Methods

        public void PostEvent<T>() where T : new()
        {
            PostEvent(new T());
        }

        public void PostEvent<T>(T evtData)
        {
            if (evtData == null)
            {
                UCLCore.Logger.LogError("", "Null event posted");
                return;
            }

            _postedEvents.Enqueue(evtData);
        }

        public void AddObserver<T>(
            OnEventPosted<T> handler,
            Predicate<T> predicate = null,
            bool once = false)
        {
            var collection = GetCollection<T>();
            if (collection == null)
            {
                collection = new ListenerCollection<T>();
                _listenerCollections.Add(typeof(T), collection);
            }

            collection.Add(new EventBusObserver<T>(handler, predicate, once));
        }

        public void RemoveObserver<T>(OnEventPosted<T> observer)
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

        public void PumpEvents(int maxEventsToProcess = -1)
        {
            var processedEvents = 0;
            while (_postedEvents.Count > 0 && processedEvents < maxEventsToProcess)
            {
                var evt = _postedEvents.Dequeue();
                if (_listenerCollections.TryGetValue(evt.GetType(), out var collection))
                {
                    collection.Publish(evt);
                }

                processedEvents++;
            }
        }

        private ListenerCollection<T> GetCollection<T>()
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