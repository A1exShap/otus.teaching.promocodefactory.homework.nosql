namespace Otus.Teaching.Pcf.Administration.DataAccess.Settings
{
    public class MongoDbSettings : IMongoSettings
    {
        public string DbName { get ; set; }
        public string ConnectionString { get ; set ; }
    }
}
