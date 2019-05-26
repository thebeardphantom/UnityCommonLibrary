using System;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Used to dynamically reference properties of a transform.
    /// </summary>
    [Flags]
    public enum TransformElement
    {
        None = 0,
        Position = 1 << 0,
        Rotation = 1 << 1,
        Scale = 1 << 2,
        All = Position | Rotation | Scale
    }
}