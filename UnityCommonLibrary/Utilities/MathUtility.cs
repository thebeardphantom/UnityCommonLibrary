using System;
using Unity.Mathematics;

namespace BeardPhantom.UCL.Utility
{
    public static class MathUtility
    {
        #region Methods

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float Spring(
            float value,
            float targetValue,
            float springSpeed,
            float springDampening,
            ref float velocity)
        {
            return Spring(
                value,
                targetValue,
                springSpeed,
                springDampening,
                ref velocity,
                UnityEngine.Time.deltaTime);
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float Spring(
            float value,
            float targetValue,
            float springSpeed,
            float springDampening,
            ref float velocity,
            float deltaTime)
        {
            velocity = math.lerp(
                velocity,
                (targetValue - value) * springDampening,
                springSpeed * deltaTime);
            return value + velocity;
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float2 Spring(
            float2 value,
            float2 targetValue,
            float2 springSpeed,
            float2 springDampening,
            ref float2 velocity)
        {
            return Spring(
                value,
                targetValue,
                springSpeed,
                springDampening,
                ref velocity,
                UnityEngine.Time.deltaTime);
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float2 Spring(
            float2 value,
            float2 targetValue,
            float2 springSpeed,
            float2 springDampening,
            ref float2 velocity,
            float2 deltaTime)
        {
            velocity = math.lerp(
                velocity,
                (targetValue - value) * springDampening,
                springSpeed * deltaTime);
            return value + velocity;
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float3 Spring(
            float3 value,
            float3 targetValue,
            float3 springSpeed,
            float3 springDampening,
            ref float3 velocity)
        {
            return Spring(
                value,
                targetValue,
                springSpeed,
                springDampening,
                ref velocity,
                UnityEngine.Time.deltaTime);
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float3 Spring(
            float3 value,
            float3 targetValue,
            float3 springSpeed,
            float3 springDampening,
            ref float3 velocity,
            float3 deltaTime)
        {
            velocity = math.lerp(
                velocity,
                (targetValue - value) * springDampening,
                springSpeed * deltaTime);
            return value + velocity;
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float4 Spring(
            float4 value,
            float4 targetValue,
            float4 springSpeed,
            float4 springDampening,
            ref float4 velocity)
        {
            return Spring(
                value,
                targetValue,
                springSpeed,
                springDampening,
                ref velocity,
                UnityEngine.Time.deltaTime);
        }

        /// <summary>
        /// Calculates a spring constraint
        /// </summary>
        public static float4 Spring(
            float4 value,
            float4 targetValue,
            float4 springSpeed,
            float4 springDampening,
            ref float4 velocity,
            float4 deltaTime)
        {
            velocity = math.lerp(
                velocity,
                (targetValue - value) * springDampening,
                springSpeed * deltaTime);
            return value + velocity;
        }

        public static float RoundTo(float vec, float nearest)
        {
            return math.round(vec * nearest) / nearest;
        }

        public static float2 RoundTo(float2 vec, float nearest)
        {
            return math.round(vec * nearest) / nearest;
        }

        public static float3 RoundTo(float3 vec, float nearest)
        {
            return math.round(vec * nearest) / nearest;
        }

        public static float4 RoundTo(float4 vec, float nearest)
        {
            return math.round(vec * nearest) / nearest;
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

        public static float Map(
            float value,
            float oldMin,
            float oldMax,
            float newMin,
            float newMax)
        {
            return (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;
        }

        public static float2 Map(
            float2 value,
            float2 oldMin,
            float2 oldMax,
            float2 newMin,
            float2 newMax)
        {
            return (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;
        }

        public static float3 Map(
            float3 value,
            float3 oldMin,
            float3 oldMax,
            float3 newMin,
            float3 newMax)
        {
            return (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;
        }

        public static float4 Map(
            float4 value,
            float4 oldMin,
            float4 oldMax,
            float4 newMin,
            float4 newMax)
        {
            return (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;
        }

        public static float Wrap(float value, float min, float max)
        {
            return Wrap(value - min, max - min) + min;
        }

        public static float Wrap(int value, int min, int max)
        {
            return Wrap(value - min, max - min) + min;
        }

        public static float Wrap(float value, float length)
        {
            var mod = value % length;
            return mod < 0f
                ? length + value
                : mod;
        }

        public static int Wrap(int value, int length)
        {
            var mod = value % length;
            return mod < 0f
                ? length + value
                : mod;
        }

        public static bool SameSign(int a, int b)
        {
            return Math.Sign(a) == Math.Sign(b);
        }

        public static bool SameSign(float a, float b)
        {
            return Math.Sign(a) == Math.Sign(b);
        }

        #endregion
    }
}