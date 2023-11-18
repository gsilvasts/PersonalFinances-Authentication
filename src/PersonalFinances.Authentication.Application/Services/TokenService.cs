using Microsoft.IdentityModel.Tokens;
using PersonalFinances.Authentication.Api.Interfaces.Services;
using PersonalFinances.Authentication.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PersonalFinances.Authentication.Application.Services
{
    [ExcludeFromCodeCoverage]
    public class TokenService : ITokenService
    {               
        public string ComputerSha256Hash(string password)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public string GenerateJWtToken(User user)
        {
            var issuer = Environment.GetEnvironmentVariable("AuthenticationSettingsIssuer") ?? string.Empty;
            var audience = Environment.GetEnvironmentVariable("AuthenticationSettingsAudience") ?? string.Empty;
            var key = Environment.GetEnvironmentVariable("AuthenticationSettingsSecretKey") ?? string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };


            if(user.Role != null && user.Role.Any())
            {
                claims.AddRange(user.Role.Select(x=> new Claim(ClaimTypes.Role, x.Trim())));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: credentials,
                claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
