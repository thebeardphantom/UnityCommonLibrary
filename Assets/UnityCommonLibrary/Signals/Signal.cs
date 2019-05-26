using System;

namespace BeardPhantom.UCL.Signals
{
    public sealed class Signal : AbstractSignal<Action>
    {
        #region Methods

        public void Publish()
        {
            if (!StartPublish())
            {
                return;
            }

            foreach (var s in Subscribers)
            {
                s.Invoke();
            }

            foreach (var s in OnceSubscribers)
            {
                s.Invoke();
            }

            StopPublish();
        }

        #endregion
    }

    public sealed class Signal<T> : AbstractSignal<Action<T>>
    {
        #region Methods

        public void Publish(T arg)
        {
            if (!StartPublish())
            {
                return;
            }

            foreach (var s in Subscribers)
            {
                s.Invoke(arg);
            }

            foreach (var s in OnceSubscribers)
            {
                s.Invoke(arg);
            }

            StopPublish();
        }

        #endregion
    }

    public sealed class Signal<T1, T2> : AbstractSignal<Action<T1, T2>>
    {
        #region Methods

        public void Publish(T1 arg1, T2 arg2)
        {
            if (!StartPublish())
            {
                return;
            }

            foreach (var s in Subscribers)
            {
                s.Invoke(arg1, arg2);
            }

            foreach (var s in OnceSubscribers)
            {
                s.Invoke(arg1, arg2);
            }

            StopPublish();
        }

        #endregion
    }

    public sealed class Signal<T1, T2, T3> : AbstractSignal<Action<T1, T2, T3>>
    {
        #region Methods

        public void Publish(T1 arg1, T2 arg2, T3 arg3)
        {
            if (!StartPublish())
            {
                return;
            }

            foreach (var s in Subscribers)
            {
                s.Invoke(arg1, arg2, arg3);
            }

            foreach (var s in OnceSubscribers)
            {
                s.Invoke(arg1, arg2, arg3);
            }

            StopPublish();
        }

        #endregion
    }

    public sealed class Signal<T1, T2, T3, T4> : AbstractSignal<Action<T1, T2, T3, T4>>
    {
        #region Methods

        public void Publish(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!StartPublish())
            {
                return;
            }

            foreach (var s in Subscribers)
            {
                s.Invoke(arg1, arg2, arg3, arg4);
            }

            foreach (var s in OnceSubscribers)
            {
                s.Invoke(arg1, arg2, arg3, arg4);
            }

            StopPublish();
        }

        #endregion
    }
}