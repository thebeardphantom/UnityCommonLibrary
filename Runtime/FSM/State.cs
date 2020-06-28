namespace BeardPhantom.UCL.FSM
{
    public abstract class State
    {
        #region Methods

        public virtual void Enter(State previousState) { }

        public virtual void Exit(State nextState) { }

        public virtual bool CanEnterState(State previousState)
        {
            return true;
        }

        #endregion
    }
}