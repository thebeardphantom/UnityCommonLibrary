using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL.Services
{
    public class ServiceKernel : IDisposable
    {
        #region Fields

        private readonly List<ServiceModule> _modules = new List<ServiceModule>();

        #endregion

        #region Methods

        public void RegisterModule(ServiceModule module)
        {
            _modules.Add(module);
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
            foreach (var module in _modules)
            {
                module.Dispose();
            }

            _modules.Clear();
        }

        public void BindAllModules()
        {
            foreach (var module in _modules)
            {
                module.BindServices();
            }

            foreach (var module in _modules)
            {
                foreach (var binding in module.Bindings.Values)
                {
                    if (binding is IPostServicesBound postServicesBound)
                    {
                        postServicesBound.OnServicesBound();
                    }
                }
            }
        }

        #endregion
    }
}