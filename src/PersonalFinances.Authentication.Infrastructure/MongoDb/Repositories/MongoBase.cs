using MongoDB.Driver;
using PersonalFinances.Authentication.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace PersonalFinances.Authentication.Infrastructure.MongoDb.Repositories
{
    [ExcludeFromCodeCoverage]
    public class MongoBase<T> where T : BaseEntity
    {
        private readonly MongoDbContext _db;
        protected readonly IMongoCollection<T> _collection;
        public MongoBase(MongoDbContext db)
        {
            _db = db;
            _collection = _db._database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public async Task CreateAsync(T entity,  CancellationToken cancellationToken)
        {
          await _collection.InsertOneAsync(entity, null, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.FindAsync(Builders<T>.Filter.Empty).Result.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            var result = await _collection.FindAsync(x=>x.Id.Equals(id));

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetByQueryAsync(Expression<Func<T, bool>> func, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(Builders<T>.Filter.Where(func), null, cancellationToken);
            return result.ToList();
        }

        public async Task DeleteOneAsync(long id, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(x=>x.Id.Equals(id), cancellationToken);
        }

        public async Task ReplaceOneAsync(T entity, CancellationToken cancellationToken)
        {
            await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
        }
    }
}
