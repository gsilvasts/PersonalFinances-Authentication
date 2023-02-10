using PersonalFinances.Authentication.Api.Models;

namespace PersonalFinances.Authentication.Api.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJWtToken(User user);
        string ComputerSha256Hash(string password);
    }
}
