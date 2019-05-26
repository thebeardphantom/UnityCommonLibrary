using System;

/// <summary>
/// Utility functions for enums
/// </summary>
public static class EnumUtility
{
    #region Methods

    /// <summary>
    /// Returns true if all bits in B are set in A
    /// </summary>
    public static bool HasAllBits<T>(this T a, T b) where T : Enum
    {
        var aLong = Convert.ToInt64(a);
        var bLong = Convert.ToInt64(b);
        return (aLong & bLong) == bLong;
    }

    /// <summary>
    /// Returns true if any bits in B are set in A
    /// </summary>
    public static bool HasAnyBits<T>(this T a, T b) where T : Enum
    {
        var aLong = Convert.ToInt64(a);
        var bLong = Convert.ToInt64(b);

        return (aLong & bLong) != 0;
    }

    #endregion
}