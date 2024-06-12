using MongoDB.Driver;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories
{
    public class MongoDbRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> collection;

        public MongoDbRepository(MongoDbContext dataContext)
        {
            collection = dataContext.Get<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => (await collection.FindAsync(_ => true)).ToList();

        public async Task<T> GetByIdAsync(Guid id)
            => (await collection.FindAsync(x => x.Id == id)).FirstOrDefault();

        public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
            => (await collection.FindAsync(predicate)).FirstOrDefault();

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
            => (await collection.FindAsync(x => ids.Contains(x.Id))).ToEnumerable();

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
            => (await collection.FindAsync(predicate)).ToEnumerable();

        public async Task AddAsync(T entity)
            => await collection.InsertOneAsync(entity);

        public async Task UpdateAsync(T entity)
            => await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

        public async Task DeleteAsync(T entity)
            => await collection.DeleteOneAsync(x => x.Id == entity.Id);
    }
}
