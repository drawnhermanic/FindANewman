using System.Collections.Generic;
using System.Linq;
using FindANewman.Domain.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace FindANewman.Data.Tests.Repositories.TestUserRepository.GetUserByEmailAddress
{
    [TestFixture(true)]
    [TestFixture(false)]
    public class WhenTestingTheMember : WhenTestingTheClass
    {
        public bool EmailAddressExists { get; set; }

        public WhenTestingTheMember(bool emailAddressExists)
        {
            EmailAddressExists = emailAddressExists;
        }

        public User Result { get; set; }
        public User ExpectedResult { get; set; }

        public string EmailAddress { get; set; }

        public IQueryable<User> QueryableItem { get; set; }

        [TestFixtureSetUp]
        public void When()
        {
            base.Setup();
            EmailAddress = "EmailAddress";
            ExpectedResult = new User { EmailAddress = EmailAddress };

            QueryableItem = new List<User>
			{
				new User(),
				new User(),
				EmailAddressExists ? ExpectedResult : new User(),				
			}.AsQueryable();

            Repository.Stub(r => r.Linq())
                .Return(QueryableItem);

            Result = ClassToTest.GetUserByEmailAddress(EmailAddress);
        }       

        [Test]
        public void ItShouldReturnTheExpectedResult()
        {
            if (EmailAddressExists)
            {
                Assert.AreEqual(ExpectedResult.EmailAddress, Result.EmailAddress);    
            }
            else
            {
                Assert.IsNull(Result);
            }
            
        }
    }
}
