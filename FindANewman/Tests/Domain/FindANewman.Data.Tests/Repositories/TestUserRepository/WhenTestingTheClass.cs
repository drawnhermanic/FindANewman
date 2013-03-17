using System;
using FindANewman.Data.Repositories;
using FindANewman.Domain.Entities;
using Rhino.Mocks;

namespace FindANewman.Data.Tests.Repositories.TestUserRepository
{
    public abstract class WhenTestingTheClass
    {
        protected UserRepository ClassToTest { get; set; }
        protected IRepository<User> Repository { get; set; }

        protected Exception Exception { get; set; }

        public void Setup()
        {
            Repository = MockRepository.GenerateMock<IRepository<User>>();

            ClassToTest = new UserRepository(Repository);
        }


    }
}
