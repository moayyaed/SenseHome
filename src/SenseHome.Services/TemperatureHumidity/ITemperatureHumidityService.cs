using System.Collections.Generic;
using System.Threading.Tasks;
using SenseHome.DataTransferObjects.TemperatureHumidity;

namespace SenseHome.Services.TemperatureHumidity
{
    public interface ITemperatureHumidityService
    {
        Task<IEnumerable<TemperatureHumidityDto>> GetAllEntriesAsync();
    }
}
