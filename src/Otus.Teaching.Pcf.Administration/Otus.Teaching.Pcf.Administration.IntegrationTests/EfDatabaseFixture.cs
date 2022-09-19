using System;
using System.Runtime;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.DataAccess.Settings;
using Otus.Teaching.Pcf.Administration.IntegrationTests.Data;

namespace Otus.Teaching.Pcf.Administration.IntegrationTests
{
    public class EfDatabaseFixture : IDisposable
    {
        private readonly EfTestDbInitializer _efTestDbInitializer;
        public IMongoDatabase _db { get; private set; }
        public AdministrationMongoDatabaseSettingsTest _settings { get; private set; }

        public EfDatabaseFixture()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");

            _db = mongoClient.GetDatabase("TestDb");
            _efTestDbInitializer = new EfTestDbInitializer(_db);
            _settings = new AdministrationMongoDatabaseSettingsTest();
            _efTestDbInitializer.InitializeDb();
        }

        public void Dispose()
        {
            _efTestDbInitializer.CleanDb();
        }
    }
}