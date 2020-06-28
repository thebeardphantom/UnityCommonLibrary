using System;

namespace BeardPhantom.UCL
{
    public delegate void EventPosted<in T>(T evtData);

    internal class EventBusObserver<T>
    {
        #region Fields

        private readonly EventPosted<T> _callback;
        public bool Once;
        protected Predicate<T> Predicate;

        #endregion

        #region Constructors

        public EventBusObserver(EventPosted<T> callback, Predicate<T> predicate, bool once)
        {
            _callback = callback;
            Predicate = predicate;
            Once = once;
        }

        #endregion

        #region Methods

        public void Publish(T evtData)
        {
            if (Predicate == null || Predicate(evtData))
            {
                _callback(evtData);
            }
        }

        /// <inheritdoc />
        public bool HasTarget(object target)
        {
            return _callback.Target == target;
        }

        public bool CallbackEquals(Delegate callback)
        {
            return callback.Equals(_callback);
        }

        #endregion
    }
}