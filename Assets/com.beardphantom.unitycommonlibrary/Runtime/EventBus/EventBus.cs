using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BeardPhantom.UCL
{
    public class EventBus
    {
        #region Types

        public delegate void OnEvent(object evt);

        #endregion

        #region Events

        public event OnEvent AnyEventPosted;

        public event OnEvent AnyEventProcessed;

        #endregion

        #region Fields

        private readonly Dictionary<Type, IListenerCollection> _listenerCollections
            = new Dictionary<Type, IListenerCollection>();

        private readonly Queue<object> _postedEvents = new Queue<object>();

        private readonly Stopwatch _processStopwatch = new Stopwatch();

        public EventBusProcessLimit ProcessLimit = new UnlimitedProcessLimit();

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
            AnyEventPosted?.Invoke(evtData);
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

        public void ProcessPostedEvents()
        {
            var processedEvents = 0;
            _processStopwatch.Restart();
            while (ProcessLimit.CanContinue(processedEvents, _processStopwatch.Elapsed))
            {
                var evt = _postedEvents.Dequeue();
                if (_listenerCollections.TryGetValue(evt.GetType(), out var collection))
                {
                    collection.NotifyListeners(evt);
                    AnyEventProcessed?.Invoke(evt);
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