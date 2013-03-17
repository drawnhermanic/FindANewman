using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindANewman.Common.Security;
using FindANewman.Data.Repositories;
using Rhino.Mocks;

namespace FindANewman.Common.Tests.Security.TestAccountMembership
{
    public abstract class WhenTestingTheClass
    {
        protected IMembershipService ClassToTest { get; set; }
        protected IUserRepository UserRepository { get; set; }

        public void Setup()
        {
            UserRepository = MockRepository.GenerateMock<IUserRepository>();
            ClassToTest = new AccountMembershipService(UserRepository);
        }
    }
}
