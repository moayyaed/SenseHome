using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenseHome.Common.Values;
using SenseHome.DataTransferObjects.MotionDetection;
using SenseHome.Services.MotionDetection;

namespace SenseHome.API.Controllers
{
    [Route("api/motion-detections")]
    [Authorize(Policy = PolicyName.Admin)]
    public class MotionDetectionController : SenseHomeBaseController
    {
        private readonly IMotionDetectionService motionDetectionService;

        public MotionDetectionController(IMotionDetectionService motionDetectionService)
        {
            this.motionDetectionService = motionDetectionService;
        }

        [HttpGet]
        public async Task<IEnumerable<MotionDetectionDto>> GetAllAsync()
        {
            var detectedMotions = await motionDetectionService.GetAllEntitiesAsync();
            return detectedMotions;
        }
    }
}
