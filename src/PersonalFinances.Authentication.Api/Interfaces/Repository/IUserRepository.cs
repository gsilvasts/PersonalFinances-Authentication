using PersonalFinances.Authentication.Api.Models;

namespace PersonalFinances.Authentication.Api.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string userName, string passwordHash);
        Task InsertrAsync(User user);
        Task UpdateAsync(User user);
    }
}
