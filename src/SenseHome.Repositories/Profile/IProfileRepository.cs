using System.Threading.Tasks;

namespace SenseHome.Repositories.Profile
{
    public interface IProfileRepository : IBaseRepository<DomainModels.Profile>
    {
        Task<DomainModels.Profile> GetByUserIdAsync(string userId);
        Task<DomainModels.Profile> GetByUserIdOrDefaultAsync(string userId);
    }
}
