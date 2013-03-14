using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.IoC.InversionOfControl
{
    public interface IIocGateway
    {
        void Initialise();

        T Resolve<T>();

        T Resolve<T>(string key);

        T Resolve<T>(object constructorArguments);

        T Resolve<T>(string key, object constructorArguments);

        object Resolve(Type type);

        object Resolve(Type type, string key);

        IEnumerable<T> ResolveAll<T>();

        Array ResolveAll(Type type);
    }
}
