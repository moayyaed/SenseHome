using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SenseHome.DataTransferObjects.MotionDetection;
using SenseHome.Repositories.MotionDetection;

namespace SenseHome.Services.MotionDetection
{
    public class MotionDetectionService : BaseService, IMotionDetectionService
    {
        private readonly IMotionDetectionRepository motionDetectionRepository;

        public MotionDetectionService(IMapper mapper, IMotionDetectionRepository motionDetectionRepository) : base(mapper)
        {
            this.motionDetectionRepository = motionDetectionRepository;
        }

        public async Task<IEnumerable<MotionDetectionDto>> GetAllEntitiesAsync()
        {
            var motionDetectios = await motionDetectionRepository.GetAllAsync();
            var motionDetectionDtos = mapper.Map<IEnumerable<MotionDetectionDto>>(motionDetectios);
            return motionDetectionDtos;
        }
    }
}
