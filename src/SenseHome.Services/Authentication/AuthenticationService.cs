using AutoMapper;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Authentication;
using SenseHome.Repositories.User;
using SenseHome.Services.Configurations;
using SenseHome.Services.UserExtension;
using System.Threading.Tasks;

namespace SenseHome.Services.Authentication
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserExtensionService userExtensionService;
        private readonly InternalCredentialConfiguration internalCredentialConfiguration;

        public AuthenticationService(
            IUserRepository userRepository,
            IUserExtensionService userExtensionService,
            InternalCredentialConfiguration internalCredentialConfiguration,
            IMapper mapper) : base(mapper)
        {
            this.userRepository = userRepository;
            this.userExtensionService = userExtensionService;
            this.internalCredentialConfiguration = internalCredentialConfiguration;
        }

        public async Task<TokenDto> InternalLoginAsync(InternalLoginDto internalLogin)
        {
            if(!internalCredentialConfiguration.IsActive)
            {
                throw new UnauthorizedException("Internal authentication is disabled");
            }
            if(internalLogin.UserName != internalCredentialConfiguration.UserName)
            {
                throw new UnauthorizedException("Invalid user name");
            }
            if(internalLogin.Password != internalCredentialConfiguration.Password)
            {
                throw new UnauthorizedException("Wrong password");
            }
            var user = new DomainModels.User
            {
                Id = "",
                IsActive = true,
                Name = internalLogin.UserName,
                Type = Common.Enums.UserType.Admin
            };

            var token = await Task.FromResult(userExtensionService.GenerateUserAccessToken(user));
            return token; 
        }

        public async Task<TokenDto> LoginAsync(UserLoginDto credential)
        {
            var user = await userRepository.GetByNameAsync(credential.Name);
            if (user == null)
            {
                throw new UnauthorizedException("No user found with this name");
            }
            if (!userExtensionService.CheckIfUserPasswordIsCorrect(credential.Password, user.Password))
            {
                throw new UnauthorizedException("Incorrect password");
            }
            return userExtensionService.GenerateUserAccessToken(user);
        }
    }
}
