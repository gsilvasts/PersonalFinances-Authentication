using PersonalFinances.Authentication.Api.Models;

namespace PersonalFinances.Authentication.Api.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User?> SignInAsync(string email, string passwordHash);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task ChangePasswordAsync(Guid id, string newPassword);
    }
}
