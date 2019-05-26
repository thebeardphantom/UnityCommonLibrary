using System;
using System.Collections.Generic;
using System.Linq;

namespace BeardPhantom.UCL.Services
{
    public class ServiceKernel : IDisposable
    {
        #region Fields

        public readonly Guid Guid = Guid.NewGuid();

        private readonly List<ServiceModule> _modules = new List<ServiceModule>();

        #endregion

        #region Constructors

        /// <inheritdoc />
        ~ServiceKernel()
        {
            ReleaseUnmanagedResources();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Binds modules
        /// </summary>
        public void RegisterModules(params ServiceModule[] modules)
        {
            foreach (var module in modules)
            {
                module.BindServices();
                _modules.Add(module);
            }

            foreach (var module in modules)
            {
                foreach (var binding in module.Bindings.Values.OfType<IService>())
                {
                    binding.OnServiceModuleBindingComplete();
                }
            }
        }

        public void RegisterModule(ServiceModule module)
        {
            module.BindServices();
            _modules.Add(module);
            foreach (var binding in module.Bindings.Values.OfType<IService>())
            {
                binding.OnServiceModuleBindingComplete();
            }
        }

        /// <summary>
        /// Retrieves a service by generic StateType.
        /// </summary>
        public TS Get<TS>() where TS : class
        {
            foreach (var module in _modules)
            {
                var instance = module.Get<TS>();
                if (instance != null)
                {
                    return instance;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            foreach (var module in _modules)
            {
                module.Dispose();
            }

            _modules.Clear();
        }

        #endregion
    }
}