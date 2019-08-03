using System;
using System.Diagnostics.CodeAnalysis;

namespace BeardPhantom.UCL.Services
{
    public class ServiceReference<T> where T : class
    {
        #region Fields

        private T _value;

        private Guid _lastRebuiltGuid;

        #endregion

        #region Properties

        [SuppressMessage("ReSharper", "MergeConditionalExpression")]
        public T Value
        {
            get
            {
                if (_value == null || ServiceLocation.KernelGuid != _lastRebuiltGuid)
                {
                    _value = ServiceLocation.Get<T>();
                    _lastRebuiltGuid = ServiceLocation.KernelGuid;
                }

                return _value;
            }
        }

        #endregion
    }
}