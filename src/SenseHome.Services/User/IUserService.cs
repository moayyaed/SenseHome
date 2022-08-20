using SenseHome.DataTransferObjects.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenseHome.Services.User
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserInsertDto user);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> UpdateAsync(UserUpdateDto user);
        Task<bool> DeleteAsync(string id);
        Task<bool> AddLog(string id);
    }
}
