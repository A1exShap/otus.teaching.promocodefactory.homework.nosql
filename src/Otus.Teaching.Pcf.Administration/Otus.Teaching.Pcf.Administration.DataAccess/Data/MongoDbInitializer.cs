using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using Otus.Teaching.Pcf.Administration.DataAccess.Settings;

namespace Otus.Teaching.Pcf.Administration.DataAccess.Data
{
    public class MongoDbInitializer : IDbInitializer
    {        
        public readonly IMongoDatabase _database;

        public MongoDbInitializer(IMongoSettings settings)
        {
            _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DbName);
            
        }

        public void InitializeDb()
        {
            var employeeCollectionName = MongoRepository<Employee>.GetCollectionName(typeof(Employee));
            _database.DropCollection(employeeCollectionName);// Замена EnsureDeleted
            var employees = _database.GetCollection<Employee>(employeeCollectionName);
            employees?.InsertMany(FakeDataFactory.Employees);
        }
    }
}
