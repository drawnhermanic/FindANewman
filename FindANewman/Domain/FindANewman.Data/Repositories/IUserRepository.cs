using System.Linq;
using FindANewman.Domain.Entities;

namespace FindANewman.Data.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmailAddress(string emailAddress);
        IQueryable<User> FindByLocation(float latitude, float longitude);
        IQueryable<User> FindAll();
        void Save(User user);
    }
}