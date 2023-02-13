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

        public async Task<User?> GetByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT * FROM [User] WHERE Id = '{id}'";

                return (await connection.QueryAsync<User>(query)).FirstOrDefault();
            }
        }

        public async Task<User?> SignInAsync(string email, string passwordHash)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM [User] WHERE Email = @email AND Password = @password";

                var parameters = new { email = email, password = passwordHash };

                return (await connection.QueryAsync<User>(query, parameters)).FirstOrDefault();
            }
        }

        public async Task InsertAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO [User] (Id, FirstName, LastName, Email, Password, Active, Role) VALUES (@Id, @FistName, @LastName, @Email, @Password, @Active, @Role)";
                var parameters = new { Id = user.Id, FistName = user.FirstName, LastName = user.LastName, Email = user.Email, Password = user.Password, Active = user.Active, Role = user.Role };

                await connection.ExecuteAsync(query, parameters);

                Task.CompletedTask.Wait();
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"UPDATE [User] " +
                    $"SET FirstName = {user.FirstName}, LastName = {user.LastName}, Email = {user.Email}, Password = {user.Password}, Active = {user.Active}, Role = {user.Role}" +
                    $"WHERE Id = {user.Id}";

                await connection.ExecuteAsync(query);

                Task.CompletedTask.Wait();
            }
        }

        public async Task ChangePasswordAsync(Guid id, string newPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"UPDATE [User] SET Password = {newPassword} WHERE Id = {id}";

                await connection.ExecuteAsync(query);

                Task.CompletedTask.Wait();
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var query = $"SELECT * FROM [User] WHERE Email = @email";

                    var parameters = new { email = email };

                    var user = (await connection.QueryAsync<User>(query, parameters)).FirstOrDefault();

                    return user;

                }
                catch (Exception e)
                {

                    throw;
                }

            }
        }
    }
}
