using System;

namespace BeardPhantom.UCL
{
    public delegate void OnEventPosted<in T>(T evtData);

    internal class EventBusObserver<T>
    {
        #region Fields

        private readonly OnEventPosted<T> _handler;

        public bool Once;

        protected Predicate<T> Predicate;

        #endregion

        #region Constructors

        public EventBusObserver(OnEventPosted<T> handler, Predicate<T> predicate, bool once)
        {
            _handler = handler;
            Predicate = predicate;
            Once = once;
        }

        #endregion

        #region Methods

        public void Publish(T evtData)
        {
            if (Predicate == null || Predicate(evtData))
            {
                _handler(evtData);
            }
        }

        public bool HasTarget(object target)
        {
            return _handler.Target == target;
        }

        public bool CallbackEquals(Delegate callback)
        {
            return callback.Equals(_handler);
        }

        #endregion
    }
}