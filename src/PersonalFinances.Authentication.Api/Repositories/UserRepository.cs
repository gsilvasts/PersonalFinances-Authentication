using Dapper;
using Microsoft.Data.SqlClient;
using PersonalFinances.Authentication.Api.Interfaces.Repository;
using PersonalFinances.Authentication.Api.Models;
using System.Data;
using System;

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

                var parameters = new { userName = userName, password = passwordHash };

                return (await connection.QueryAsync<User>(query, parameters)).FirstOrDefault();
            }
        }

        public async Task InsertrAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO User (Id, UserName, FistName, LastName, Email, Password, Active, Role) VALUES (@Id, @UserName, @FistName, @LastName, @Email, @Password, @Active, @Role)";
                var parameters = new { Id = user.Id, UserName = user.UserName, FistName = user.FirstName, LastName = user.LastName, Email = user.Email, Password = user.Password, Active = user.Active, Role = user.Role };

                await connection.ExecuteAsync(query, parameters);

                Task.CompletedTask.Wait();
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO User (UserName, FistName, LastName, Email, Password, Active, Role) VALUES (@UserName, @FistName, @LastName, @Email, @Password, @Active, @Role)";
                var parameters = new { UserName = user.UserName, FistName = user.FirstName, LastName = user.LastName, Email = user.Email, Password = user.Password, Active = user.Active, Role = user.Role };
                await connection.ExecuteAsync(query, parameters);

                Task.CompletedTask.Wait();
            }
        }
    }
}
