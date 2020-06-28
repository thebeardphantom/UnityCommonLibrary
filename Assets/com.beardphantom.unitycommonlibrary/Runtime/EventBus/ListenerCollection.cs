using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    internal interface IListenerCollection
    {
        #region Methods

        void RemoveTarget(object target);

        void Publish(object data);

        #endregion
    }

    internal class ListenerCollection<T> : IListenerCollection
    {
        #region Fields

        private readonly List<EventBusObserver<T>> _observers =
            new List<EventBusObserver<T>>();

        #endregion

        #region Methods

        public void Add(EventBusObserver<T> observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }
        }

        public void Remove(Delegate callback)
        {
            RemoveWhere(observer => observer.CallbackEquals(callback));
        }

        /// <inheritdoc />
        public void RemoveTarget(object target)
        {
            RemoveWhere(o => o.HasTarget(target));
        }

        /// <inheritdoc />
        public void Publish(object data)
        {
            var typedData = (T) data;
            Publish(typedData);
        }

        private void RemoveWhere(Predicate<EventBusObserver<T>> predicate)
        {
            for (var i = _observers.Count - 1; i >= 0; i--)
            {
                var observer = _observers[i];
                if (predicate(observer))
                {
                    _observers.RemoveAt(i);
                }
            }
        }

        private void Publish(T evtData)
        {
            for (var i = _observers.Count - 1; i >= 0; i--)
            {
                var observer = _observers[i];
                observer.Publish(evtData);
                if (observer.Once)
                {
                    _observers.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}