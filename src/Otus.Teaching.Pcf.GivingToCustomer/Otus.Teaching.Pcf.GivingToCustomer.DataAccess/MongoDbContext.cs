using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess
{
    public class MongoDbContext : DbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;
        
        public MongoDbContext(MongoClient client, IMongoDatabase database)
        {
            _client = client;
            _database = database;
        }

        public MongoClient Client => _client;

        public new IMongoDatabase Database => _database;

        public IMongoCollection<T> Get<T>()
            => Database.GetCollection<T>($"{typeof(T).Name.ToLower()}s")
                ?? throw new InvalidOperationException("Неизвестный тип для коллекции: " + typeof(T).Name);
    }
}
