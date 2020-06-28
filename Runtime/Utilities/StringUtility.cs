using System;
using System.Text;

namespace BeardPhantom.UCL.Utility
{
    public static class StringUtility
    {
        #region Methods

        public static string FormatStr(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static bool IsNullOrWhitespace(this string str)
        {
            if (str == null)
            {
                return true;
            }

            for (var i = 0; i < str.Length; i++)
            {
                if (!char.IsWhiteSpace(str[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ContainsIgnoreCase(this string str, string other)
        {
            return str.IndexOf(other, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static StringBuilder AppendLineFormat(
            this StringBuilder builder,
            string format,
            params object[] args)
        {
            return builder.AppendLine(string.Format(format, args));
        }

        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
            if (sb == null || sb.Length == 0)
            {
                return sb;
            }

            var i = sb.Length - 1;

            for (; i >= 0; i--)
            {
                if (!char.IsWhiteSpace(sb[i]))
                {
                    break;
                }
            }

            if (i < sb.Length - 1)
            {
                sb.Length = i + 1;
            }

            return sb;
        }

        #endregion
    }
}