using System;
using System.Collections;
using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// Interface for a job executor
    /// </summary>
    public interface IJobsHost
    {
        #region Methods

        /// <summary>
        /// Stops a coroutine.
        /// </summary>
        void HaltCoroutine(Coroutine routine);

        /// <summary>
        /// Executes a given coroutine
        /// </summary>
        Coroutine ExecuteCoroutine(IEnumerator routine);

        /// <summary>
        /// Executes a given function after a delay in seconds, unscaled.
        /// </summary>
        Coroutine ExecuteInSeconds(float delay, Action action);

        /// <summary>
        /// Executes a given function after a delay in seconds, scaled.
        /// </summary>
        Coroutine ExecuteInSecondsScaled(float delay, Action action);

        /// <summary>
        /// Executes a given function next frame.
        /// </summary>
        Coroutine ExecuteNextFrame(Action action);

        /// <summary>
        /// Executes a given function after a specified number of frames have passed.
        /// </summary>
        Coroutine ExecuteInFrames(int frames, Action action);

        #endregion
    }
}