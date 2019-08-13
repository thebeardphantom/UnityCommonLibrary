using System;
using System.Collections.Generic;
using BeardPhantom.UCL.Time;

namespace BeardPhantom.UCL.FSM
{
    public sealed class EnumStateMachine<T> where T : struct, IFormattable, IConvertible, IComparable
    {
        #region Types

        public delegate void OnEnter(T? previousState);

        public delegate void OnExit(T nextState);

        public delegate void OnStateChanged(T? previousState, T nextState);

        #endregion

        #region Events

        public event OnStateChanged StateChanged;

        #endregion

        #region Fields

        private readonly Dictionary<T, List<OnEnter>> _onStateEnter =
            new Dictionary<T, List<OnEnter>>();

        private readonly Dictionary<T, List<OnExit>> _onStateExit =
            new Dictionary<T, List<OnExit>>();

        private readonly Dictionary<T, List<Action>> _onStateUpdate =
            new Dictionary<T, List<Action>>();

        #endregion

        #region Properties

        public T? CurrentState { get; private set; }

        public T? PreviousState { get; private set; }

        public TimeSlice StateEnterTime { get; private set; }

        #endregion

        #region Constructors

        public EnumStateMachine()
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be Enum");
            }
        }

        #endregion

        #region Methods

        public void Update()
        {
            if (!CurrentState.HasValue)
            {
                return;
            }

            if (_onStateUpdate.TryGetValue(CurrentState.Value, out var callbacks))
            {
                for (var i = callbacks.Count - 1; i >= 0; i--)
                {
                    var callback = callbacks[i];
                    callback();
                }
            }
        }

        public void AddOnTick(T state, Action onUpdate)
        {
            if (!_onStateUpdate.TryGetValue(state, out var callbacks))
            {
                callbacks = new List<Action>();
                _onStateUpdate.Add(state, callbacks);
            }

            callbacks.Add(onUpdate);
        }

        public void AddOnEnter(T state, OnEnter onEnter)
        {
            if (!_onStateEnter.TryGetValue(state, out var callbacks))
            {
                callbacks = new List<OnEnter>();
                _onStateEnter.Add(state, callbacks);
            }

            callbacks.Add(onEnter);
        }

        public void AddOnExit(T state, OnExit onExit)
        {
            if (!_onStateExit.TryGetValue(state, out var callbacks))
            {
                callbacks = new List<OnExit>();
                _onStateExit.Add(state, callbacks);
            }

            callbacks.Add(onExit);
        }

        public void ChangeState(T nextState)
        {
            if (CurrentState.HasValue && Equals(nextState, CurrentState.Value))
            {
                return;
            }

            if (CurrentState.HasValue && _onStateExit.TryGetValue(CurrentState.Value, out var exitCallbacks))
            {
                foreach (var callback in exitCallbacks)
                {
                    callback(nextState);
                }
            }

            PreviousState = CurrentState;
            CurrentState = nextState;

            if (_onStateEnter.TryGetValue(CurrentState.Value, out var enterCallbacks))
            {
                foreach (var callback in enterCallbacks)
                {
                    callback(PreviousState);
                }
            }

            StateEnterTime = TimeSlice.Create();
            StateChanged?.Invoke(PreviousState, CurrentState.Value);
        }

        /// <inheritdoc />
        public void RemoveCallbacks(object target)
        {
            foreach (var list in _onStateEnter.Values)
            {
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Target == target)
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            foreach (var list in _onStateExit.Values)
            {
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Target == target)
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            foreach (var list in _onStateUpdate.Values)
            {
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Target == target)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }

        #endregion
    }
}