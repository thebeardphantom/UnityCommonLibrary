using System;
using System.Collections;
using System.Collections.Generic;
using BeardPhantom.UCL.Pooling;

namespace BeardPhantom.UCL
{
    internal interface IListenerCollection { }

    internal class ListenerCollection<T> : IListenerCollection, IEnumerable<EventBusObserver<T>>
    {
        #region Fields

        private readonly HashSet<EventBusObserver<T>> _observers =
            new HashSet<EventBusObserver<T>>();

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
            lock (_observers)
            {
                _observers.RemoveWhere(observer => observer.CallbackEquals(callback));
            }
        }

        public void RemoveWhere(Predicate<EventBusObserver<T>> match)
        {
            lock (_observers)
            {
                _observers.RemoveWhere(match);
            }
        }

        public IEnumerator<EventBusObserver<T>> GetEnumerator()
        {
            using (var list = ListPool<EventBusObserver<T>>.Obtain())
            {
                list.Collection.AddRange(_observers);
                foreach (var observer in list.Collection)
                {
                    yield return observer;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}