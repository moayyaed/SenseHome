using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SenseHome.Common.Exceptions;
using SenseHome.DB.Mongo;

namespace SenseHome.Repositories.Profile
{
    public class ProfileRepository : BaseRepository<DomainModels.Profile>, IProfileRepository
    {
        public ProfileRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.Profile>("profiles");
        }

        public async Task<DomainModels.Profile> GetByUserIdAsync(string userId)
        {
            var cursor = await collection.FindAsync(p => p.UserId == userId);
            var profile = cursor.FirstOrDefault();
            if (profile == null)
            {
                throw new NotFoundException($"No profile exists with UserID {userId}");
            }
            return profile;
        }

        public async Task<DomainModels.Profile> GetByUserIdOrDefaultAsync(string userId)
        {
            var cursor = await collection.FindAsync(p => p.UserId == userId);
            return cursor.FirstOrDefault();
        }
    }
}
