using System;
using System.Collections.Generic;

namespace Common.IoC.InversionOfControl
{
    public static class Ioc
    {
        private static IIocGateway _gateway;

        public static void Initialise(IIocGateway gateway)
        {
            _gateway = gateway;
            gateway.Initialise();
        }

        public static T Resolve<T>()
        {
            return _gateway.Resolve<T>();
        }

        public static T Resolve<T>(string key)
        {
            return _gateway.Resolve<T>(key);
        }

        public static T Resolve<T>(string key, object constructorArguments)
        {
            return _gateway.Resolve<T>(key, constructorArguments);
        }

        public static object Resolve(Type type)
        {
            return _gateway.Resolve(type);
        }

        public static object Resolve(Type type, string key)
        {
            return _gateway.Resolve(type, key);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return _gateway.ResolveAll<T>();
        }

        public static Array ResolveAll(Type type)
        {
            return _gateway.ResolveAll(type);
        }
    }
}
