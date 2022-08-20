using System;
using System.Threading.Tasks;

namespace SenseHome.Repositories.User
{
    public interface IUserRepository : IBaseRepository<DomainModels.User>
    {
        Task<DomainModels.User> GetByNameAsync(string name);
        Task<DomainModels.User> GetByNameOrDefaultAsync(string name);
        Task<bool> AddLog(string id, DateTime dateTime);
    }
}
