using Microsoft.Extensions.DependencyInjection;
using SenseHome.Repositories.MotionDetection;
using SenseHome.Repositories.Profile;
using SenseHome.Repositories.Subscription;
using SenseHome.Repositories.TemperatureHumidity;
using SenseHome.Repositories.User;
using SenseHome.Services.Authentication;
using SenseHome.Services.MotionDetection;
using SenseHome.Services.Profile;
using SenseHome.Services.Subscription;
using SenseHome.Services.TemperatureHumidity;
using SenseHome.Services.User;
using SenseHome.Services.UserExtension;

namespace SenseHome.API.Configuration
{
    public static class DependencyConfiguration
    {
        public static void AddDependentServicesAndRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserExtensionService, UserExtensionService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<ITemperatureHumidityRepository, TemperatureHumidityRepository>();
            services.AddSingleton<ITemperatureHumidityService, TemperatureHumidityService>();

            services.AddSingleton<IProfileRepository, ProfileRepository>();
            services.AddSingleton<IProfileService, ProfileService>();

            services.AddSingleton<IMotionDetectionRepository, MotionDetectionRepository>();
            services.AddSingleton<IMotionDetectionService, MotionDetectionService>();

            services.AddSingleton<ISubscriptionRepository, SubscriptionRepository>();
            services.AddSingleton<ISubscriptionService, SubscriptionService>();
        }
    }
}
