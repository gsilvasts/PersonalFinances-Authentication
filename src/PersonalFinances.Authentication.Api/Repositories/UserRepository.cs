using Dapper;
using Microsoft.Data.SqlClient;
using PersonalFinances.Authentication.Api.Interfaces.Repository;
using PersonalFinances.Authentication.Api.Models;

namespace PersonalFinances.Authentication.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CONNECTIONSTRING");
        }

        public async Task<User?> GetAsync(string userName, string passwordHash)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM User WHERE UserName = @userName AND Password = @password";

                var parameters = new {userName = userName, password = passwordHash};

                return (await connection.QueryAsync<User>(query, parameters)).FirstOrDefault();
            }
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
