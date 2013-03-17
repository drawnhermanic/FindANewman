using FindANewman.Domain.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace FindANewman.Data.Tests.Repositories.TestUserRepository.FindAll
{
    public class WhenTestingTheMember : WhenTestingTheClass
    {
        public User User { get; set; }

        [TestFixtureSetUp]
        public void When()
        {
            base.Setup();

            User = new User();
            ClassToTest.FindAll();
        }       

        [Test]
        public void ItShouldSaveTheUser()
        {
            Repository.AssertWasCalled(r => r.FindAll());
        }
    }
}
