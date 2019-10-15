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

        private readonly List<GatedEventBusEvent> _pendingGatedEvents
            = new List<GatedEventBusEvent>();

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

            if (evtData is GatedEventBusEvent gatedEvent)
            {
                _pendingGatedEvents.Add(gatedEvent);
            }
            else
            {
                _readyEvents.Enqueue(evtData);
            }
        }

        public void PostEventNow<T>() where T : EventBusEvent, new()
        {
            PostEventNow(new T());
        }

        public void PostEventNow<T>(T evt) where T : EventBusEvent
        {
            switch (evt)
            {
                case null:
                {
                    UCLCore.Logger.LogError("", "Null event posted");
                    return;
                }
                case GatedEventBusEvent _:
                {
                    throw new ArgumentException("Gated events cannot use PostEventNow");
                }
                default:
                {
                    PostEventNowInternal(evt);
                    break;
                }
            }
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

        public void Process()
        {
            while (_readyEvents.Count > 0)
            {
                var evt = _readyEvents.Dequeue();
                PostEventNowInternal(evt);
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

        private void PostEventNowInternal(EventBusEvent evt)
        {
            if (_listenerCollections.TryGetValue(evt.GetType(), out var collection))
            {
                collection.Publish(evt);
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