using SenseHome.DB.Mongo;

namespace SenseHome.Repositories.TemperatureHumidity
{
    public class TemperatureHumidityRepository : BaseRepository<DomainModels.TemperatureHumidity>, ITemperatureHumidityRepository
    {
        public TemperatureHumidityRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.TemperatureHumidity>("temperatureHumidities");
        }
    }
}
