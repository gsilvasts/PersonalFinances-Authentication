using PersonalFinances.Authentication.Api.Interfaces.Repository;
using PersonalFinances.Authentication.Api.Models;

namespace PersonalFinances.Authentication.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetAsync(string userName, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task InsertrAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
