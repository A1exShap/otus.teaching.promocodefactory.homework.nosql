using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using Otus.Teaching.Pcf.Administration.DataAccess.Settings;

namespace Otus.Teaching.Pcf.Administration.WebHost
{
    public static class ServiceBuilder
    {
        public static IServiceCollection GetServiceCollection(IServiceCollection services = null, IConfiguration configuration = null)
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }
            if(configuration ==null)
            {
                configuration = GetConfiguration();
            }
            services.AddControllers().AddMvcOptions(x =>
                x.SuppressAsyncSuffixInActionNames = false);
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings")); // получаем конфигурацию подлкючения к MongoDb
            services.AddSingleton<IMongoSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value); // DI для MongoDb
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IDbInitializer, MongoDbInitializer>();


            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
            return services;
        }

        public static IConfiguration GetConfiguration()
        {
           var configuration = new  ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration;
        }
    }

}
