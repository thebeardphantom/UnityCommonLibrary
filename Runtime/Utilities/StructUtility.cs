namespace BeardPhantom.UCL.Utility
{
    public static class StructUtility
    {
        #region Methods

        public static bool IsDefaultValue<T>(this T value) where T : struct
        {
            return default(T).Equals(value);
        }

        #endregion
    }
}