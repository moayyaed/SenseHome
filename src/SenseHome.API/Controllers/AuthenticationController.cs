using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SenseHome.DataTransferObjects.Authentication;
using SenseHome.Services.Authentication;

namespace SenseHome.API.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : SenseHomeBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> LoginAsync([FromBody] UserLoginDto userLogin)
        {
            var token = await authenticationService.LoginAsync(userLogin);
            return Ok(token);
        }

        [HttpPost("internal")]
        public async Task<ActionResult<TokenDto>> InternalLoginAsync([FromBody] InternalLoginDto internalLogin)
        {
            var token = await authenticationService.InternalLoginAsync(internalLogin);
            return Ok(token);
        }
    }
}
