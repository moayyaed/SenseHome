using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenseHome.Common.Values;
using SenseHome.DataTransferObjects.User;
using SenseHome.Services.User;

namespace SenseHome.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UserController : SenseHomeBaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            var user = await userService.GetUserByIdAsync(userId);
            return Ok(user);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<UserDto>> GetAsync([FromRoute] string id)
        {
            var user =  await userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<UserDto>> CreateAsync([FromBody] UserInsertDto user)
        {
            var createdUser = await userService.CreateUserAsync(user);
            return Created($"/api/users/${createdUser.Id}", createdUser);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<UserDto>> UpdateAsync([FromBody] UserUpdateDto user, [FromRoute] string id)
        {
            if(id != user.Id)
            {
                return BadRequest("User id didn't matched");
            }
            var updatedUser = await userService.UpdateAsync(user);
            return Ok(updatedUser);
        }

        [HttpGet]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }
    }
}
