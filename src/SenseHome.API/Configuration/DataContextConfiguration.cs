using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenseHome.DB.Mongo;

namespace SenseHome.API.Configuration
{
    public static class DataContextConfiguration
    {
        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            //Binding MongoDBSettings from appsettings.json and add as a singleton
            var mongoDBSettings = new MongoDBSettings();
            configuration.GetSection(nameof(MongoDBSettings)).Bind(mongoDBSettings);
            services.AddSingleton(mongoDBSettings);
            // injecting MongoDBContext
            services.AddSingleton<MongoDBContext>();
        }
    }
}
