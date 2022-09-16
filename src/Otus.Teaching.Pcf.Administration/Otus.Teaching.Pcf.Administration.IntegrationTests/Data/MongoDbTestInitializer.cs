using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;

namespace Otus.Teaching.Pcf.Administration.IntegrationTests.Data
{
    public class MongoDbTestInitializer : IDbInitializer
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Employee> _employees;
        private readonly string _employeeCollectionName;
        public MongoDbTestInitializer(IMongoDatabase database)
        {
            _database = database;
            _employeeCollectionName = MongoRepository<Employee>.GetCollectionName(typeof(Employee));
            _employees = _database.GetCollection<Employee>(_employeeCollectionName);
        }
        public void InitializeDb()
        {
            _database.DropCollection(_employeeCollectionName);
            _employees?.InsertMany(TestDataFactory.Employees);
        }

        public void CleanDb()
        {
           
            _database.DropCollection(_employeeCollectionName);           
            
        }
    }
}
