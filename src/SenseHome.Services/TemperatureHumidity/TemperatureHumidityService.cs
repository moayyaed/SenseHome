using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SenseHome.DataTransferObjects.TemperatureHumidity;
using SenseHome.Repositories.TemperatureHumidity;

namespace SenseHome.Services.TemperatureHumidity
{
    public class TemperatureHumidityService : BaseService, ITemperatureHumidityService
    {
        private readonly ITemperatureHumidityRepository temperatureHumidityRepository;

        public TemperatureHumidityService(
            ITemperatureHumidityRepository temperatureHumidityRepository,
            IMapper mapper) : base(mapper)
        {
            this.temperatureHumidityRepository = temperatureHumidityRepository;
        }

        public async Task<IEnumerable<TemperatureHumidityDto>> GetAllEntriesAsync()
        {
            var temperatureHumidities = await temperatureHumidityRepository.GetAllAsync();
            var tempratureHumidityDtos = mapper.Map<IEnumerable<TemperatureHumidityDto>>(temperatureHumidities);
            return tempratureHumidityDtos;
        }
    }
}
