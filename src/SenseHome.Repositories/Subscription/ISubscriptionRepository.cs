using System.Threading.Tasks;

namespace SenseHome.Repositories.Subscription
{
    public interface ISubscriptionRepository : IBaseRepository<DomainModels.Subscription>
    {
        Task<DomainModels.Subscription> GetByUserIdAsync(string userId);
        Task<DomainModels.Subscription> GetByUserIdOrDefaultAsync(string userId);
    }
}
