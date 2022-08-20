using SenseHome.DataTransferObjects.Authentication;

namespace SenseHome.Services.UserExtension
{
    public interface IUserExtensionService
    {
        string GetUserHashedPassword(string rawPassword);
        bool CheckIfUserPasswordIsCorrect(string rawPassword, string hashedPassowrd);
        TokenDto GenerateUserAccessToken(DomainModels.User user);
    }
}
