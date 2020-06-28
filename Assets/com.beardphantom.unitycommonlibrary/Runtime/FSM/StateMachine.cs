using System;
using System.Collections.Generic;
using BeardPhantom.UCL.Signals;

namespace BeardPhantom.UCL.FSM
{
    public class StateMachine
    {
        #region Fields

        public readonly Signal<State> StateChanged = new Signal<State>();

        private readonly Dictionary<Type, State> _stateLookup = new Dictionary<Type, State>();

        private ITickableState _currentTickable;

        #endregion

        #region Properties

        public State CurrentState { get; private set; }

        #endregion

        #region Constructors

        public StateMachine(params State[] states)
        {
            if (states == null || states.Length == 0)
            {
                UCLCore.Logger.LogError("", "No states defined");
            }

            foreach (var s in states)
            {
                _stateLookup.Add(s.GetType(), s);
            }

            var nullState = new NullState();
            _stateLookup.Add(nullState.GetType(), nullState);
            CurrentState = nullState;
            CurrentState.Enter(nullState);
        }

        #endregion

        #region Methods

        public T GetState<T>() where T : State
        {
            return (T) GetState(typeof(T));
        }

        public State GetState(Type type)
        {
            _stateLookup.TryGetValue(type, out var nextState);

            return nextState;
        }

        public T ChangeState<T>() where T : State
        {
            return (T) ChangeState(typeof(T));
        }

        public State ChangeState(Type type)
        {
            var nextState = GetState(type);

            if (nextState == null)
            {
                UCLCore.Logger.LogError("", $"State {type.Name} is not defined in state collection");
            }

            if (ReferenceEquals(CurrentState, nextState))
            {
                return CurrentState;
            }

            if (CurrentState is NullState || nextState.CanEnterState(CurrentState))
            {
                CurrentState.Exit(nextState);
                var oldState = CurrentState;
                CurrentState = nextState;
                _currentTickable = nextState as ITickableState;
                CurrentState.Enter(oldState);
                StateChanged.Publish(CurrentState);
            }
            else
            {
                UCLCore.Logger.LogError("", $"Cannot transition from {CurrentState} to {nextState}");
            }

            return CurrentState;
        }

        public void Tick()
        {
            _currentTickable?.Tick();
        }

        public override string ToString()
        {
            return $"State: {CurrentState.GetType().Name}";
        }

        #endregion
    }
}