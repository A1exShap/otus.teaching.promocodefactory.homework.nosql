using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data
{
    public class MongoDbInitializer : IDbInitializer
    {
        private readonly MongoDbContext _dataContext;

        public MongoDbInitializer(MongoDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InitializeDb()
        {
            _dataContext.Client.DropDatabase(_dataContext.Database.DatabaseNamespace.DatabaseName);

            _dataContext.Get<Customer>().InsertMany(FakeDataFactory.Customers);
            _dataContext.Get<Preference>().InsertMany(FakeDataFactory.Preferences);
        }
    }
}
