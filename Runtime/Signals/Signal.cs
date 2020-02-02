using System.Collections.Generic;

namespace BeardPhantom.UCL.Signals
{
    public delegate void SignalCallback<in T>(T data);

    public class Signal<T>
    {
        #region Fields

        private readonly List<SignalCallback<T>> _subscribers = new List<SignalCallback<T>>();

        private readonly List<SignalCallback<T>> _onceSubscribers = new List<SignalCallback<T>>();

        private readonly List<SignalCallback<T>> _subscribersStaging = new List<SignalCallback<T>>();

        private readonly List<SignalCallback<T>> _onceSubscribersStaging = new List<SignalCallback<T>>();

        public bool Enabled = true;

        private bool _isPublishing;

        #endregion

        #region Methods

        private static bool ShouldRemoveCallback(SignalCallback<T> callback)
        {
            return callback.Target == null && !callback.Method.IsStatic;
        }

        public void Clear()
        {
            _subscribers.Clear();
            _subscribersStaging.Clear();
            _onceSubscribers.Clear();
            _onceSubscribersStaging.Clear();
        }

        public void Subscribe(SignalCallback<T> subscriber)
        {
            if (_isPublishing)
            {
                _subscribersStaging.Add(subscriber);
            }
            else
            {
                Unsubscribe(subscriber);
                _subscribers.Add(subscriber);
            }
        }

        public void SubscribeOnce(SignalCallback<T> subscriber)
        {
            if (_isPublishing)
            {
                _onceSubscribersStaging.Add(subscriber);
            }
            else
            {
                Unsubscribe(subscriber);
                _onceSubscribers.Add(subscriber);
            }
        }

        public void Unsubscribe(SignalCallback<T> subscriber)
        {
            _subscribers.Remove(subscriber);
            _onceSubscribers.Remove(subscriber);
        }

        public void UnsubscribeTarget(object target)
        {
            _subscribers.RemoveAll(s => Equals(s?.Target, target));
        }

        public void Publish(T data)
        {
            if (!StartPublish())
            {
                return;
            }

            foreach (var s in _subscribers)
            {
                s.Invoke(data);
            }

            foreach (var s in _onceSubscribers)
            {
                s.Invoke(data);
            }

            StopPublish();
        }

        private bool StartPublish()
        {
            if (Enabled)
            {
                _isPublishing = true;
                _subscribers.RemoveAll(ShouldRemoveCallback);
                _onceSubscribers.RemoveAll(ShouldRemoveCallback);
            }

            return Enabled;
        }

        private void StopPublish()
        {
            _isPublishing = false;
            _onceSubscribers.Clear();

            foreach (var s in _subscribersStaging)
            {
                Subscribe(s);
            }

            _subscribersStaging.Clear();

            foreach (var s in _onceSubscribersStaging)
            {
                SubscribeOnce(s);
            }

            _onceSubscribersStaging.Clear();
        }

        #endregion
    }
}