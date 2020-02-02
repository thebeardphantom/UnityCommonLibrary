namespace BeardPhantom.UCL.Time
{
    public struct TimeSlice
    {
        #region Properties

        public int FrameCount { get; private set; }

        public float RealtimeSinceStartup { get; private set; }

        public float Time { get; private set; }

        public float UnscaledTime { get; private set; }

        #endregion

        #region Methods

        public static TimeSlice Create()
        {
            return new TimeSlice
            {
                Time = UnityEngine.Time.time,
                UnscaledTime = UnityEngine.Time.unscaledTime,
                RealtimeSinceStartup = UnityEngine.Time.realtimeSinceStartup,
                FrameCount = UnityEngine.Time.frameCount
            };
        }

        #endregion
    }
}