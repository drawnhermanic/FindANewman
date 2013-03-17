using System;
using System.Linq;
using FindANewman.Domain.Entities;

namespace FindANewman.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IRepository<User> Repository { get; set; }

        public UserRepository(IRepository<User> repository)
        {
            Repository = repository;
        }

        public User GetUserByEmailAddress(string emailAddress)
        {
            return Repository.Linq().FirstOrDefault(item => item.EmailAddress == emailAddress);
        }

        public IQueryable<User> FindByLocation(float latitude, float longitude)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FindAll()
        {
            return Repository.FindAll();
        }

        public void Save(User user)
        {
            Repository.Save(user);
        }
    }
}
