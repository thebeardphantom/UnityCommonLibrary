using System;
using System.Collections;
using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Endpoint for allowing any class to execute coroutines.
    /// </summary>
    public class Jobs : MonoBehaviour, IJobsHost
    {
        #region Methods

        /// <summary>
        /// Internal implementation for delaying a function call.
        /// </summary>
        private static IEnumerator ExecuteDelayedInternal(float delay, Action action)
        {
            var startTime = UnityEngine.Time.unscaledTime;
            while (UnityEngine.Time.unscaledTime - startTime < delay)
            {
                yield return null;
            }

            action();
        }

        /// <summary>
        /// Internal implementation for delaying a function call in scaled time.
        /// </summary>
        private static IEnumerator ExecuteDelayedScaledInternal(float delay, Action action)
        {
            var startTime = UnityEngine.Time.time;
            while (UnityEngine.Time.time - startTime < delay)
            {
                yield return null;
            }

            action();
        }

        /// <summary>
        /// Internal implementation for delaying a function call by frames.
        /// </summary>
        private static IEnumerator ExecuteInFramesInternal(int frames, Action action)
        {
            for (var i = 0; i < frames; i++)
            {
                yield return null;
            }

            action();
        }

        /// <summary>
        /// Stops a coroutine.
        /// </summary>
        public void HaltCoroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }

        /// <summary>
        /// Executes a given coroutine
        /// </summary>
        public Coroutine ExecuteCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        /// <summary>
        /// Executes a given function after a delay in seconds, unscaled.
        /// </summary>
        public Coroutine ExecuteInSeconds(float delay, Action action)
        {
            return StartCoroutine(ExecuteDelayedInternal(delay, action));
        }

        /// <summary>
        /// Executes a given function after a delay in seconds, scaled.
        /// </summary>
        public Coroutine ExecuteInSecondsScaled(float delay, Action action)
        {
            return StartCoroutine(ExecuteDelayedScaledInternal(delay, action));
        }

        /// <summary>
        /// Executes a given function next frame.
        /// </summary>
        public Coroutine ExecuteNextFrame(Action action)
        {
            return StartCoroutine(ExecuteInFramesInternal(1, action));
        }

        /// <summary>
        /// Executes a given function after a specified number of frames have passed.
        /// </summary>
        public Coroutine ExecuteInFrames(int frames, Action action)
        {
            return StartCoroutine(ExecuteInFramesInternal(frames, action));
        }

        #endregion
    }
}