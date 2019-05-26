using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public class Relay
    {
        #region Fields

        private readonly HashSet<Action> _onComplete = new HashSet<Action>();

        #endregion

        #region Properties

        public bool IsComplete { get; private set; }

        #endregion

        #region Methods

        public void OnComplete(Action onComplete)
        {
            if (IsComplete)
            {
                onComplete();
            }
            else
            {
                _onComplete.Add(onComplete);
            }
        }

        public void Complete()
        {
            if (!IsComplete)
            {
                IsComplete = true;
                foreach (var act in _onComplete)
                {
                    act.Invoke();
                }
            }
        }

        #endregion
    }

    public class Relay<T>
    {
        #region Fields

        private readonly HashSet<Action<T>> _onComplete = new HashSet<Action<T>>();

        #endregion

        #region Properties

        public bool IsComplete { get; private set; }

        public T Value { get; private set; }

        #endregion

        #region Methods

        public void OnComplete(Action<T> onComplete)
        {
            if (IsComplete)
            {
                onComplete(Value);
            }
            else
            {
                _onComplete.Add(onComplete);
            }
        }

        public void Complete(T value)
        {
            if (!IsComplete)
            {
                Value = value;
                IsComplete = true;
                foreach (var act in _onComplete)
                {
                    act.Invoke(value);
                }
            }
        }

        #endregion
    }
}