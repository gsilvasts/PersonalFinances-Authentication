using Microsoft.Extensions.DependencyInjection;
using PersonalFinances.Authentication.Api.Interfaces.Services;
using PersonalFinances.Authentication.Application.Interfaces.Services;
using PersonalFinances.Authentication.Application.Services;

namespace PersonalFinances.Authentication.Application
{
    public static class Setup
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IUserService, UserService>();

            return service;
        }
    }
}
