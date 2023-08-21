using PersonalFinances.Authentication.Application.Models.InputModels;
using PersonalFinances.Authentication.Application.Models.OutputModel;
using PersonalFinances.Authentication.Domain.Entities;

namespace PersonalFinances.Authentication.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserOutputModel> SignUpAsync(SignUpInputModel user, CancellationToken cancellationToken);
        Task<SignInOutputModel> SignInAsync(string email, string password, CancellationToken cancellationToken);
        Task<User> SignOutAsync(string email, string password, CancellationToken cancellationToken);
        Task<UserOutputModel> ChangePasswordAsync(string email, string oldPassword, string newPassword, CancellationToken cancellationToken);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
    }
}
