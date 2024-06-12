using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories;
using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration["DbType"] ?? string.Empty;

            if (dbType.Equals("MongoDB"))
            {
                var mongoDbConnectionString = configuration.GetConnectionString("PromocodeFactoryGivingToCustomerMongoDb");
                var mongoClient = new MongoClient(mongoDbConnectionString);
                var mongoDbName = new MongoUrl(mongoDbConnectionString).DatabaseName;
                var database = mongoClient.GetDatabase(mongoDbName);

                services.AddSingleton(mongoClient);
                services.AddSingleton(database);
                services.AddScoped<MongoDbContext>();

                services.AddScoped<IDbInitializer, MongoDbInitializer>();
                services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));
            }
            else if (dbType.Equals("PostgreSQL"))
            {
                services.AddDbContext<DataContext>(x =>
                {
                    x.UseNpgsql(configuration.GetConnectionString("PromocodeFactoryGivingToCustomerDb"));
                    x.UseSnakeCaseNamingConvention();
                    x.UseLazyLoadingProxies();
                });

                services.AddScoped<IDbInitializer, EfDbInitializer>();
                services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            }
            else throw new InvalidOperationException("Указан некорректный тип СУБД");

            return services;
        }
    }
}
