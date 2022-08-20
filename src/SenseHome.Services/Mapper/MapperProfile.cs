using SenseHome.DataTransferObjects.User;
using SenseHome.DataTransferObjects.Profile;
using SenseHome.DataTransferObjects.Subscription;
using SenseHome.DataTransferObjects.TemperatureHumidity;

namespace SenseHome.Services.Mapper
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateUserMaps();
            CreateProfileMaps();
            CreateSubscriptionMaps();
            CreateTemperatureHumidityMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<DomainModels.User, UserDto>();
            CreateMap<UserInsertDto, DomainModels.User>();
            CreateMap<UserUpdateDto, DomainModels.User>();
        }

        private void CreateProfileMaps()
        {
            CreateMap<DomainModels.Profile, ProfileDto>();
            CreateMap<ProfileUpsertDto, DomainModels.Profile>();
        }

        private void CreateSubscriptionMaps()
        {
            CreateMap<DomainModels.Subscription, SubscriptionDto>();
            CreateMap<SubscriptionInsertDto, DomainModels.Subscription>();
            CreateMap<SubscriptionUpdateDto, DomainModels.Subscription>();

        }

        private void CreateTemperatureHumidityMaps()
        {
            CreateMap<DomainModels.TemperatureHumidity, TemperatureHumidityDto>();
        }
    }
}
