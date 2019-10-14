using System;

namespace BeardPhantom.UCL
{
    public delegate void EventPosted<in T>(T evtData);

    public delegate void EventPostedNoData<in T>();

    internal abstract class EventBusObserverBase<T>
    {
        #region Fields

        public bool Once;

        protected Predicate<T> Predicate;

        #endregion

        #region Methods

        public abstract void Publish(T evtData);

        public abstract bool HasTarget(object target);

        public abstract bool CallbackEquals(Delegate callback);

        #endregion
    }

    internal class EventBusObserver<T> : EventBusObserverBase<T>
    {
        #region Fields

        private readonly EventPosted<T> _callback;

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

        public override void Publish(T evtData)
        {
            if (Predicate == null || Predicate(evtData))
            {
                _callback(evtData);
            }
        }

        /// <inheritdoc />
        public override bool HasTarget(object target)
        {
            return _callback.Target == target;
        }

        public override bool CallbackEquals(Delegate callback)
        {
            return callback.Equals(_callback);
        }

        #endregion
    }

    internal class EventBusObserverNoData<T> : EventBusObserverBase<T>
    {
        #region Fields

        private readonly EventPostedNoData<T> _callback;

        #endregion

        #region Constructors

        public EventBusObserverNoData(EventPostedNoData<T> callback, Predicate<T> predicate, bool once)
        {
            _callback = callback;
            Predicate = predicate;
            Once = once;
        }

        #endregion

        #region Methods

        public override bool CallbackEquals(Delegate callback)
        {
            return callback.Equals(_callback);
        }

        /// <inheritdoc />
        public override void Publish(T evtData)
        {
            if (Predicate == null || Predicate(evtData))
            {
                _callback();
            }
        }

        public override bool HasTarget(object target)
        {
            return _callback.Target == target;
        }

        #endregion
    }
}