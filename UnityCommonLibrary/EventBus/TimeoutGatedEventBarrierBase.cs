namespace BeardPhantom.UCL
{
    public abstract class TimeoutGatedEventBarrierBase : IGatedEventBarrier
    {
        #region Fields

        private readonly float _duration;

        private readonly float _startTime;

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool Complete => GetTime() - _startTime >= _duration;

        #endregion

        #region Constructors

        protected TimeoutGatedEventBarrierBase(float duration)
        {
            _duration = duration;
            _startTime = GetTime();
        }

        #endregion

        #region Methods

        protected abstract float GetTime();

        #endregion
    }
}