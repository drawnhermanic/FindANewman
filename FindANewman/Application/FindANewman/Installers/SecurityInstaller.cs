using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FindANewman.Common.Security;

namespace FindANewman.Installers
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IMembershipService)).ImplementedBy(typeof(AccountMembershipService)).LifeStyle.Singleton);
        }
    }
}