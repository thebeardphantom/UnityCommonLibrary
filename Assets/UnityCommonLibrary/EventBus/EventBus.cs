using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public delegate void EventPosted<in T>(T evtData);

    public class EventBus
    {
        #region Fields

        private readonly ConcurrentDictionary<Type, IListenerCollection> _listenerCollections
            = new ConcurrentDictionary<Type, IListenerCollection>();

        private readonly Stack<EventBusEventData> _evtStack = new Stack<EventBusEventData>();

        #endregion

        #region Methods

        public void PostEvent<T>(T evtData) where T : EventBusEventData
        {
            if (evtData == null)
            {
                UCLCore.Logger.LogError("", "Null event posted");
                return;
            }

            var collection = GetCollection<T>();
            if (collection != null)
            {
                if (_evtStack.Count > 0)
                {
                    evtData.Source = evtData.Source ?? _evtStack.Peek();
                }

                _evtStack.Push(evtData);
                foreach (var observer in collection)
                {
                    observer.Publish(evtData);
                }

                _evtStack.Pop();
                collection.RemoveWhere(observer => observer.Once);
            }
        }

        public void RegisterObserver<T>(
            EventPosted<T> callback,
            Predicate<T> predicate = null,
            bool once = false)
        {
            var collection = GetCollection<T>();
            if (collection == null)
            {
                collection = new ListenerCollection<T>();
                _listenerCollections.TryAdd(typeof(T), collection);
            }

            collection.Add(new EventBusObserver<T>(callback, predicate, once));
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