using System;
using System.Collections.Generic;
using Castle.Windsor;
using Common.IoC.InversionOfControl;

namespace Common.IoC
{
    public abstract class CastleIocGateway : IIocGateway
    {
        private IWindsorContainer _container;

        /// <summary>
        /// Initialises the underlying IoC implementation.
        /// </summary>
        public void Initialise()
        {
            if (_container != null)
            {
                _container.Dispose();
            }

            _container = new WindsorContainer();

            RegisterComponents(_container);
        }

        /// <summary>
        /// Registers the components on the newly created container.
        /// </summary>
        /// <param name="container">The container to register the components on.</param>
        protected abstract void RegisterComponents(IWindsorContainer container);

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return _container.Resolve<T>(key);
        }

        public T Resolve<T>(object constructorArguments)
        {
            return _container.Resolve<T>(constructorArguments);
        }

        public T Resolve<T>(string key, object constructorArguments)
        {
            return _container.Resolve<T>(key, constructorArguments);
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public object Resolve(Type type, string key)
        {
            return _container.Resolve(type, key);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.ResolveAll<T>();
        }

        public Array ResolveAll(Type type)
        {
            return _container.ResolveAll(type);
        }        
    }
}
