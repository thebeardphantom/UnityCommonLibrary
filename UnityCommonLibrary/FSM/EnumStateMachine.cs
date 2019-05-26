using System;
using System.Collections.Generic;
using BeardPhantom.UCL.Signals;
using BeardPhantom.UCL.Time;

namespace BeardPhantom.UCL.FSM
{
    public sealed class EnumStateMachine<T>
        where T : struct, IFormattable, IConvertible, IComparable
    {
        #region Types

        public delegate void OnEnter(T previousState);

        public delegate void OnExit(T nextState);

        #endregion

        #region Fields

        public readonly Signal<T, T> StateChanged = new Signal<T, T>();

        private readonly Dictionary<T, HashSet<OnEnter>> OnStateEnter =
            new Dictionary<T, HashSet<OnEnter>>();

        private readonly Dictionary<T, HashSet<OnExit>> OnStateExit =
            new Dictionary<T, HashSet<OnExit>>();

        private readonly Dictionary<T, HashSet<Action>> OnStateUpdate =
            new Dictionary<T, HashSet<Action>>();

        #endregion

        #region Properties

        public T CurrentState { get; private set; }

        public T PreviousState { get; private set; }

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
            if (OnStateUpdate.TryGetValue(CurrentState, out var callbacks))
            {
                foreach (var a in callbacks)
                {
                    a();
                }
            }
        }

        public void AddOnUpdate(T state, Action onUpdate)
        {
            if (!OnStateUpdate.TryGetValue(state, out var callbacks))
            {
                callbacks = new HashSet<Action>();
                OnStateUpdate.Add(state, callbacks);
            }

            callbacks.Add(onUpdate);
        }

        public void AddOnEnter(T state, OnEnter onEnter)
        {
            if (!OnStateEnter.TryGetValue(state, out var callbacks))
            {
                callbacks = new HashSet<OnEnter>();
                OnStateEnter.Add(state, callbacks);
            }

            callbacks.Add(onEnter);
        }

        public void AddOnExit(T state, OnExit onExit)
        {
            if (!OnStateExit.TryGetValue(state, out var callbacks))
            {
                callbacks = new HashSet<OnExit>();
                OnStateExit.Add(state, callbacks);
            }

            callbacks.Add(onExit);
        }

        public void ChangeState(T nextState)
        {
            if (Equals(nextState, CurrentState))
            {
                return;
            }

            if (OnStateExit.TryGetValue(CurrentState, out var exitCallbacks))
            {
                foreach (var callback in exitCallbacks)
                {
                    callback(nextState);
                }
            }

            PreviousState = CurrentState;
            CurrentState = nextState;

            if (OnStateEnter.TryGetValue(CurrentState, out var enterCallbacks))
            {
                foreach (var callback in enterCallbacks)
                {
                    callback(PreviousState);
                }
            }

            StateEnterTime = TimeSlice.Create();
            StateChanged.Publish(PreviousState, CurrentState);
        }

        /// <inheritdoc />
        public void RemoveCallbacks(object obj)
        {
            foreach (var list in OnStateEnter.Values)
            {
                list.RemoveWhere(m => m.Target == obj);
            }

            foreach (var list in OnStateExit.Values)
            {
                list.RemoveWhere(m => m.Target == obj);
            }

            foreach (var list in OnStateUpdate.Values)
            {
                list.RemoveWhere(m => m.Target == obj);
            }
        }

        #endregion
    }
}