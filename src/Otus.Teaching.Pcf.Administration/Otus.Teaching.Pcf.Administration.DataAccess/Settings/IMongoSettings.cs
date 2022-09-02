namespace Otus.Teaching.Pcf.Administration.DataAccess.Settings
{
    public interface IMongoSettings
    {
        string DbName { get; set; }
        string ConnectionString { get; set; }
    }
}
