using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenseHome.Common.Values;
using SenseHome.DataTransferObjects.Profile;
using SenseHome.Services.Profile;

namespace SenseHome.API.Controllers
{
    [Route("api")]
    [Authorize]
    public class ProfileController : SenseHomeBaseController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpPost("profiles")]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<ProfileDto>> CreateAsync([FromBody] ProfileUpsertDto profile)
        {
            var createdProfile = await profileService.CreateProfileAsync(profile);
            return Created($"/api/users/${createdProfile.UserId}/profile", createdProfile);
        }

        [HttpPut("profiles/{id}")]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<ProfileDto>> UpdateAsync([FromRoute]string id, [FromBody]ProfileUpsertDto profile)
        {
            if (id != profile.Id)
            {
                return BadRequest("Profile Id didn't matched with route Id");
            }
            var updatedProfile = await profileService.UpdateProfileAsync(profile);
            return Ok(updatedProfile);
        }

        [HttpGet("users/{id}/profile")]
        [Authorize(Policy = PolicyName.AdminOrAppliances)]
        public async Task<ActionResult<ProfileDto>> GetByUserIdAsync(string id)
        {
            var profile = await profileService.GetProfileByUserIdAsync(id);
            return Ok(profile);
        }
    }
}
