using System;

namespace BeardPhantom.UCL
{
    [Flags]
    public enum RenderProxyOptions
    {
        UseUniqueMeshInstance = 1 << 0,
        UseUniqueMaterialInstance = 1 << 1,
        LogUnsupportedRendererTypes = 1 << 2,
        FindInactiveRenderers = 1 << 3,
    }

    public static class RenderProxyOptionsExtensions
    {
        #region Methods

        public static bool HasFlagFast(this RenderProxyOptions value, RenderProxyOptions flag)
        {
            return (value & flag) != 0;
        }

        #endregion
    }
}