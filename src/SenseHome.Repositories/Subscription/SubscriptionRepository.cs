using System.Threading.Tasks;
using MongoDB.Driver;
using SenseHome.Common.Exceptions;
using SenseHome.DB.Mongo;

namespace SenseHome.Repositories.Subscription
{
    public class SubscriptionRepository : BaseRepository<DomainModels.Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.Subscription>("subscriptions");
        }

        public async Task<DomainModels.Subscription> GetByUserIdAsync(string userId)
        {
            var subscription = await GetByUserIdOrDefaultAsync(userId);
            if(subscription == null)
            {
                throw new NotFoundException("No subscription found for this user");
            }
            return subscription;
        }

        public async Task<DomainModels.Subscription> GetByUserIdOrDefaultAsync(string userId)
        {
            var cursor = await collection.FindAsync(s => s.UserId == userId);
            var subscription = await cursor.FirstOrDefaultAsync();
            return subscription;
        }
    }
}
