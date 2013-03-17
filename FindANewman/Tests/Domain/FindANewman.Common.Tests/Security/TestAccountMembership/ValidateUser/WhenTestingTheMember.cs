using FindANewman.Common.Security;
using FindANewman.Domain.Entities;
using NUnit.Framework;
using Rhino.Mocks;

namespace FindANewman.Common.Tests.Security.TestAccountMembership.ValidateUser
{
    [TestFixture(true, MembershipValidationResult.Success)]
    [TestFixture(false, MembershipValidationResult.InvalidCredentials)]
    public class WhenTestingTheMember : WhenTestingTheClass
    {
        public bool UserExists { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        protected MembershipValidationResult Result { get; set; }
        protected MembershipValidationResult ExpectedResult { get; set; }

        
        public WhenTestingTheMember(bool userExists, MembershipValidationResult expectedResult)
        {
            UserExists = userExists;
            ExpectedResult = expectedResult;
        }

        [TestFixtureSetUp]
        public void When()
        {
            Setup();
            UserRepository.Stub(r => r.GetUserByEmailAddress(Username)).Return(UserExists ? new User() : null);
            Result = ClassToTest.ValidateUser(Username, Password);
        }

        [Test]
        public void ItShouldReturnTheExpectedResult()
        {
            Assert.AreEqual(ExpectedResult, Result);
        }
    }
}


