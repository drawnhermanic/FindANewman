using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FindANewman.Domain.ErrorProcessing;
using FindANewman.Domain.Logging;
using FindANewman.Exception;

namespace FindANewman.Installers
{
    public class ErrorHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IExceptionHandler>().ImplementedBy<ExceptionHandler>());
            container.Register(Component.For<IExceptionDataProvider>().ImplementedBy<ExceptionDataProvider>());
            container.Register(Component.For<IErrorProcessor>().ImplementedBy<ErrorProcessor>());
            container.Register(Component.For<IExceptionLogger>().ImplementedBy<ExceptionLogger>());
        }
    }
}