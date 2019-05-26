using System;
using System.Collections.Generic;
using System.Text;
using BeardPhantom.UCL.Utility;

namespace BeardPhantom.UCL.Signals
{
    public abstract class AbstractSignal<T> : ISignal where T : class
    {
        #region Fields

        protected readonly List<T> Subscribers = new List<T>();

        protected readonly List<T> OnceSubscribers = new List<T>();

        protected readonly List<T> SubscribersStaging = new List<T>();

        protected readonly List<T> OnceSubscribersStaging = new List<T>();

        public bool Enabled = true;

        protected bool IsPublishing;

        #endregion

        #region Properties

        public string SubscriberList
        {
            get
            {
                if (Subscribers.Count == 0)
                {
                    return "";
                }

                var sb = new StringBuilder();

                foreach (var s in Subscribers)
                {
                    var del = s as Delegate;
                    sb.AppendLineFormat("{0}.{1}", del?.Target, del?.Method.Name);
                }

                sb.TrimEnd();

                return sb.ToString();
            }
        }

        #endregion

        #region Constructors

        protected AbstractSignal()
        {
            if (!typeof(T).IsSubclassOf(typeof(Delegate)))
            {
                throw new ArgumentException("T must be of StateType Delegate.");
            }

            SignalUtility.AllMessages.Add(this);
        }

        ~AbstractSignal()
        {
            SignalUtility.AllMessages.Remove(this);
        }

        #endregion

        #region Methods

        protected static bool ShouldRemoveCallback(T t)
        {
            var del = t as Delegate;
            return del != null && del.Target == null && !del.Method.IsStatic;
        }

        public void Clear()
        {
            Subscribers.Clear();
            SubscribersStaging.Clear();
            OnceSubscribers.Clear();
            OnceSubscribersStaging.Clear();
        }

        public void Subscribe(T subscriber)
        {
            if (IsPublishing)
            {
                SubscribersStaging.Add(subscriber);
            }
            else
            {
                Unsubscribe(subscriber);
                Subscribers.Add(subscriber);
            }
        }

        public void SubscribeOnce(T subscriber)
        {
            if (IsPublishing)
            {
                OnceSubscribersStaging.Add(subscriber);
            }
            else
            {
                Unsubscribe(subscriber);
                OnceSubscribers.Add(subscriber);
            }
        }

        public void Unsubscribe(T subscriber)
        {
            Subscribers.Remove(subscriber);
            OnceSubscribers.Remove(subscriber);
        }

        public void UnsubscribeTarget(object target)
        {
            Subscribers.RemoveAll(s => Equals((s as Delegate)?.Target, target));
        }

        protected bool StartPublish()
        {
            if (Enabled)
            {
                IsPublishing = true;
                Subscribers.RemoveAll(ShouldRemoveCallback);
                OnceSubscribers.RemoveAll(ShouldRemoveCallback);
            }

            return Enabled;
        }

        protected void StopPublish()
        {
            IsPublishing = false;
            OnceSubscribers.Clear();

            foreach (var s in SubscribersStaging)
            {
                Subscribe(s);
            }

            SubscribersStaging.Clear();

            foreach (var s in OnceSubscribersStaging)
            {
                SubscribeOnce(s);
            }

            OnceSubscribersStaging.Clear();
        }

        #endregion
    }
}