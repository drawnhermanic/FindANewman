using System;
using System.Globalization;
using FindANewman.Data.Repositories;

namespace FindANewman.Common.Security
{
    public class AccountMembershipService : IMembershipService
    {
        public IUserRepository UserRepository { get; set; }

        public AccountMembershipService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public MembershipValidationResult ValidateUser(string username, string password)
        {
            var user = UserRepository.GetUserByEmailAddress(username);

            return user != null ? MembershipValidationResult.Success : MembershipValidationResult.InvalidCredentials;
        }
    }
}