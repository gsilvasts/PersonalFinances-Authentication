using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PersonalFinances.Authentication.Domain.Interfaces.Repository;
using PersonalFinances.Authentication.Infrastructure.MongoDb;
using PersonalFinances.Authentication.Infrastructure.Repositories;

namespace PersonalFinances.Authentication.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            MongoDbContext.ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? string.Empty;
            MongoDbContext.DataBase = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? string.Empty;
            MongoDbContext.IsSSL = Convert.ToBoolean(Environment.GetEnvironmentVariable("MONGO_ISSSL"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbContext>>().Value);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
