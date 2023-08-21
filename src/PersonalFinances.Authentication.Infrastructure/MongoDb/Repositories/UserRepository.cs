using MongoDB.Driver;
using PersonalFinances.Authentication.Domain.Entities;
using PersonalFinances.Authentication.Domain.Interfaces.Repository;
using PersonalFinances.Authentication.Infrastructure.MongoDb;
using PersonalFinances.Authentication.Infrastructure.MongoDb.Repositories;

namespace PersonalFinances.Authentication.Infrastructure.Repositories
{
    public class UserRepository : MongoBase<User>, IUserRepository
    {
        public UserRepository(MongoDbContext db) : base(db)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var result = await GetByQueryAsync(x => x.Email == email, cancellationToken);

            return result.FirstOrDefault();
        }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken)
        {
            var result = await GetByQueryAsync(x => x.Email == email && x.Password == password, cancellationToken);

            return result.FirstOrDefault();
        }

        public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            await CreateAsync(user, cancellationToken);

        }

        public async Task ChangePasswordAsync(User user, CancellationToken cancellationToken)
        {
            var builder = Builders<User>.Filter.Where(x => x.Id == user.Id);
            var definition = Builders<User>.Update
                .Set(x => x.Password, user.Password);

            await _collection.UpdateOneAsync(builder, definition, null, cancellationToken);            
        }
    }
}
