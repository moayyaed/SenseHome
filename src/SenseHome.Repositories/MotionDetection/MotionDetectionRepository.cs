using SenseHome.DB.Mongo;

namespace SenseHome.Repositories.MotionDetection
{
    public class MotionDetectionRepository : BaseRepository<DomainModels.MotionDetection>, IMotionDetectionRepository
    {
        public MotionDetectionRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.MotionDetection>("motionDetections");
        }
    }
}
