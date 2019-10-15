using System;

namespace BeardPhantom.UCL
{
    public class WaitUntilGatedEventBarrier<T> : IGatedEventBarrier
    {
        #region Fields

        private readonly Func<bool> _func;

        #endregion

        #region Properties

        /// <inheritdoc />
        public bool Complete => _func();

        #endregion

        #region Constructors

        public WaitUntilGatedEventBarrier(Func<bool> func)
        {
            _func = func;
        }

        #endregion
    }
}