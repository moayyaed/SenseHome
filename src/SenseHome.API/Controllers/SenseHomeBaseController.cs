using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SenseHome.Common.Enums;

namespace SenseHome.API.Controllers
{
    [ApiController]
    public class SenseHomeBaseController : ControllerBase
    {
        protected string GetCurrentUserId()
        {
            var claimUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return claimUserId;
        }

        protected UserType GetCurrentUserRole()
        {
            var claimUserRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            int.TryParse(claimUserRole, out var userRole);
            return (UserType)(userRole);
        }

    }
}
