using System.Collections.Generic;
using UnityEngine.Assertions;

namespace BeardPhantom.UCL
{
    internal interface IListenerCollection
    {
        #region Methods

        void RemoveTarget(object target);

        void Publish(object data);

        #endregion
    }

    internal class ListenerCollection<T> : IListenerCollection where T : EventBusEventData
    {
        #region Fields

        private readonly HashSet<EventBusObserverBase<T>> _observers =
            new HashSet<EventBusObserverBase<T>>();

        #endregion

        #region Methods

        public void Add(EventBusObserver<T> observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }
        }

        public void Remove(EventPosted<T> callback)
        {
            _observers.RemoveWhere(observer => observer.CallbackEquals(callback));
        }

        /// <inheritdoc />
        public void RemoveTarget(object target)
        {
            _observers.RemoveWhere(o => o.HasTarget(target));
        }

        /// <inheritdoc />
        public void Publish(object data)
        {
            var typedData = data as T;
            Assert.IsNotNull(typedData, $"data is not of type {typeof(T)}");
            Publish(typedData);
        }

        private void Publish(T evtData)
        {
            foreach (var observer in _observers)
            {
                observer.Publish(evtData);
            }

            _observers.RemoveWhere(o => o.Once);
        }

        #endregion
    }
}