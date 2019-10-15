namespace BeardPhantom.UCL
{
    public class UnscaledTimeoutGatedEventBarrier : TimeoutGatedEventBarrierBase
    {
        #region Constructors

        /// <inheritdoc />
        public UnscaledTimeoutGatedEventBarrier(float duration) : base(duration) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override float GetTime()
        {
            return UnityEngine.Time.unscaledTime;
        }

        #endregion
    }
}