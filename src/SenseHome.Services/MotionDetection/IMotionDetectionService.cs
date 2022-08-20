using System.Collections.Generic;
using System.Threading.Tasks;
using SenseHome.DataTransferObjects.MotionDetection;

namespace SenseHome.Services.MotionDetection
{
    public interface IMotionDetectionService
    {
        Task<IEnumerable<MotionDetectionDto>> GetAllEntitiesAsync();
    }
}
