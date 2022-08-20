using SenseHome.DataTransferObjects.Authentication;
using System.Threading.Tasks;

namespace SenseHome.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenDto> LoginAsync(UserLoginDto loginDto);
        Task<TokenDto> InternalLoginAsync(InternalLoginDto internalLogin);
    }
}
