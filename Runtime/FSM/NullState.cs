namespace BeardPhantom.UCL.FSM
{
    public sealed class NullState : State
    {
        #region Methods

        public override void Enter(State previousState) { }

        public override void Exit(State nextState) { }

        #endregion
    }
}