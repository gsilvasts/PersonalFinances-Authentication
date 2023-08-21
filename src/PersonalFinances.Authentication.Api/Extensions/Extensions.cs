using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PersonalFinances.Authentication.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAuthnAuthz(this IServiceCollection services)
        {
            var issuer = Environment.GetEnvironmentVariable("AuthenticationSettingsIssuer");
            var audience = Environment.GetEnvironmentVariable("AuthenticationSettingsAudience");
            var key = Environment.GetEnvironmentVariable("AuthenticationSettingsSecretKey");

            #region Authn
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });
            #endregion

            #region Authz   
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("manager"));
            });
            #endregion

            return services;
        }
    }
}
