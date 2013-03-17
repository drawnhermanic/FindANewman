using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FindANewman.Data.Repositories;
using FindANewman.Data.Repositories.Generic;

namespace FindANewman.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)).LifeStyle.Singleton);
            container.Register(Component.For(typeof(IUserRepository)).ImplementedBy(typeof(UserRepository)).LifeStyle.Singleton);
            container.Register(Component.For(typeof(IDataExecutionContext<>)).ImplementedBy(typeof(DataExecutionContext<>)).LifeStyle.Singleton);

        }
    }
}