namespace BeardPhantom.UCL.Time
{
    /// <summary>
    /// An enumeration for describing how time should be measured.
    /// </summary>
    public enum TimeMode
    {
        Time,
        UnscaledTime,
        RealtimeSinceStartup,
        FixedTime,
        DeltaTime,
        UnscaledDeltaTime,
        SmoothDeltaTime,
        FixedDeltaTime,
        TimeSinceLevelLoad,
        One
    }
}