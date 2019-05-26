using System;
using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class MathUtility
    {
        #region Methods

        /// <summary>
        /// Checks if a and b have the same sign.
        /// </summary>
        public static bool SameSign(float a, float b)
        {
            return Math.Sign(a) == Math.Sign(b);
        }

        /// <summary>
        /// Whether a is approximately equal to b
        /// </summary>
        public static bool Approximately(double a, double b)
        {
            return Math.Abs(b - a) >= double.Epsilon;
        }

        /// <summary>
        /// Wraps value between begin (inclusive) and end (exclusive)
        /// </summary>
        public static float Wrap(float value, float begin, float end)
        {
            return Wrap(value - begin, end - begin) + begin;
        }

        /// <summary>
        /// Wraps value between 0 (inclusive) and length (exclusive)
        /// </summary>
        public static float Wrap(float value, float length)
        {
            var mod = Mathf.Repeat(value, length);
            return mod < 0f
                ? length + value
                : mod;
        }

        /// <summary>
        /// Wraps value between begin (inclusive) and end (exclusive)
        /// </summary>
        public static int Wrap(int value, int begin, int end)
        {
            return Wrap(value, end - begin) + begin;
        }

        /// <summary>
        /// Wraps value between 0 (inclusive) and length (exclusive)
        /// </summary>
        public static int Wrap(int value, int length)
        {
            var mod = value % length;
            return mod < 0f
                ? length + value
                : mod;
        }

        /// <summary>
        /// Clamps a float to the -1...1 inclusive range
        /// </summary>
        /// <param name="f"></param>
        /// <returns>A normalized float.</returns>
        public static float Normalize(float f)
        {
            if (f > 1f)
            {
                return 1f;
            }

            if (f < -1f)
            {
                return -1f;
            }

            return f;
        }

        public static float SignOrZero(float f)
        {
            return Mathf.Approximately(0f, f)
                ? 0f
                : Mathf.Sign(f);
        }

        public static float RoundTo(float f, float nearest)
        {
            var multiple = 1f / nearest;

            return (float) Math.Round(
                    f * multiple,
                    MidpointRounding.AwayFromZero)
                / multiple;
        }

        public static float Map(
            float value,
            float oldMin,
            float oldMax,
            float newMin,
            float newMax)
        {
            return (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;
        }

        #endregion

        #region Min

        public static byte Min(params byte[] values)
        {
            var min = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                min = values[i] < min
                    ? values[i]
                    : min;
            }

            return min;
        }

        public static sbyte Min(params sbyte[] values)
        {
            var min = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                min = values[i] < min
                    ? values[i]
                    : min;
            }

            return min;
        }

        public static uint Min(params uint[] values)
        {
            var min = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                min = values[i] < min
                    ? values[i]
                    : min;
            }

            return min;
        }

        public static ushort Min(params ushort[] values)
        {
            var min = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                min = values[i] < min
                    ? values[i]
                    : min;
            }

            return min;
        }

        public static short Min(params short[] values)
        {
            var min = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                min = values[i] < min
                    ? values[i]
                    : min;
            }

            return min;
        }

        #endregion

        #region Max

        public static byte Max(params byte[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        public static sbyte Max(params sbyte[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        public static uint Max(params uint[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        public static ushort Max(params ushort[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        public static short Max(params short[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        public static long Max(params long[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        public static ulong Max(params ulong[] values)
        {
            var max = values[0];

            for (var i = 1; i < values.Length; i++)
            {
                max = values[i] > max
                    ? values[i]
                    : max;
            }

            return max;
        }

        #endregion

        #region Clamp

        public static byte Clamp(byte current, byte min, byte max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static sbyte Clamp(sbyte current, sbyte min, sbyte max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static uint Clamp(uint current, uint min, uint max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static ushort Clamp(ushort current, ushort min, ushort max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static short Clamp(short current, short min, short max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static long Clamp(long current, long min, long max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static ulong Clamp(ulong current, ulong min, ulong max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        public static char Clamp(char current, char min, char max)
        {
            if (current > max)
            {
                return max;
            }

            return current < min
                ? min
                : current;
        }

        #endregion
    }
}