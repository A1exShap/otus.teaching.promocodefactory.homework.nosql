using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.DataAccess.Settings;
using Otus.Teaching.Pcf.Administration.IntegrationTests.Data;
using Otus.Teaching.Pcf.Administration.WebHost;
using System;

namespace Otus.Teaching.Pcf.Administration.IntegrationTests
{
    public class MongoDbFixture : IDisposable
    {
        public MongoDbSettings MongoDbSettings { get; set; }
        private readonly MongoDbTestInitializer _mongoDbTestInitializer;
        public MongoDbFixture()
        {
            MongoDbSettings = ServiceBuilder.GetConfiguration()
                .GetSection("MongoDbSettings")
                .Get<MongoDbSettings>();
            var database = new MongoClient(MongoDbSettings.ConnectionString).GetDatabase(MongoDbSettings.DbName);
            _mongoDbTestInitializer = new MongoDbTestInitializer(database);
            _mongoDbTestInitializer.InitializeDb();

        }

        public void Dispose()
        {
            _mongoDbTestInitializer.CleanDb();
        }
    }
}
