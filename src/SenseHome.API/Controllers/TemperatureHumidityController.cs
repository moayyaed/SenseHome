using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenseHome.Common.Values;
using SenseHome.DataTransferObjects.TemperatureHumidity;
using SenseHome.Services.TemperatureHumidity;

namespace SenseHome.API.Controllers
{
    [Route("api/temperature-humidities")]
    [Authorize(Policy = PolicyName.Admin)]
    public class TemperatureHumidityController : SenseHomeBaseController
    {
        private readonly ITemperatureHumidityService temperatureHumidityService;

        public TemperatureHumidityController(ITemperatureHumidityService temperatureHumidityService)
        {
            this.temperatureHumidityService = temperatureHumidityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemperatureHumidityDto>>> GetAllAsync()
        {
            var temperatureHumidities = await temperatureHumidityService.GetAllEntriesAsync();
            return Ok(temperatureHumidities);
        }
    }
}
