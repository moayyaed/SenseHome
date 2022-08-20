using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SenseHome.DomainModels.Base;

namespace SenseHome.Repositories
{
    public interface IBaseRepository<T>  where T : BaseEntity
    {
        Task<T> GetAsync(string id);
        Task<T> GetOrDefaultAsync(string id);
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> DeleteAsync(string id);
        IQueryable<T> GetAsQueryable();
        Task<T> UpdateAsync(T entity);
    }
}
