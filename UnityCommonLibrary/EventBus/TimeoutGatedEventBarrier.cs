namespace BeardPhantom.UCL
{
    public class TimeoutGatedEventBarrier : TimeoutGatedEventBarrierBase
    {
        #region Constructors

        /// <inheritdoc />
        public TimeoutGatedEventBarrier(float duration) : base(duration) { }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override float GetTime()
        {
            return UnityEngine.Time.time;
        }

        #endregion
    }
}