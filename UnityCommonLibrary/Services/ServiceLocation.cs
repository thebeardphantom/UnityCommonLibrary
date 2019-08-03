using System;

namespace BeardPhantom.UCL.Services
{
    public static class ServiceLocation
    {
        #region Fields

        /// <summary>
        /// Accessor for current service locator
        /// </summary>
        private static ServiceKernel _kernel;

        #endregion

        #region Properties

        public static Guid KernelGuid { get; private set; }

        #endregion

        #region Methods

        public static void SetKernel(ServiceKernel kernel)
        {
            _kernel?.Dispose();
            _kernel = kernel;
            KernelGuid = Guid.NewGuid();
            kernel.BindAllModules();
        }

        public static T Get<T>() where T : class
        {
            return _kernel.Get<T>();
        }

        #endregion
    }
}