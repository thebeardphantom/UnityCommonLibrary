using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class RichTextUtility
    {
        #region Methods

        public static string MakeColored(this string s, Color c)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(c)}>{s}</color>";
        }

        public static string MakeBold(this string s)
        {
            return $"<b>{s}</b>";
        }

        public static string MakeItalic(this string s)
        {
            return $"<i>{s}</i>";
        }

        #endregion
    }
}