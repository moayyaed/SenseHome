using System.Threading.Tasks;
using SenseHome.DataTransferObjects.Profile;

namespace SenseHome.Services.Profile
{
    public interface IProfileService
    {
        Task<ProfileDto> GetProfileByUserIdAsync(string userId);
        Task<ProfileDto> CreateProfileAsync(ProfileUpsertDto profile);
        Task<ProfileDto> UpdateProfileAsync(ProfileUpsertDto profile);
    }
}
