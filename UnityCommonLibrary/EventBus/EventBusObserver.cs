using System;

namespace BeardPhantom.UCL
{
    internal class EventBusObserver<T>
    {
        #region Fields

        public readonly bool Once;

        private readonly EventPosted<T> _callback;

        private readonly Predicate<T> _predicate;

        #endregion

        #region Constructors

        public EventBusObserver(EventPosted<T> callback, Predicate<T> predicate, bool once)
        {
            _callback = callback;
            _predicate = predicate;
            Once = once;
        }

        #endregion

        #region Methods

        public void Publish(T evtData)
        {
            if (_predicate == null || _predicate(evtData))
            {
                _callback(evtData);
            }
        }

        public bool CallbackEquals(EventPosted<T> callback)
        {
            return callback.Equals(_callback);
        }

        #endregion
    }
}