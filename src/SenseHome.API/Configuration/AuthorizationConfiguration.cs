using Microsoft.Extensions.DependencyInjection;
using SenseHome.Common.Enums;
using SenseHome.Common.Values;

namespace SenseHome.API.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static void AddSenseHomeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                option.AddPolicy(PolicyName.Admin, config =>
                {
                    config.RequireRole(UserType.Admin.ToIntegerString());
                });

                option.AddPolicy(PolicyName.AdminOrAppliances, config =>
                {
                    config.RequireRole(UserType.Admin.ToIntegerString(), UserType.Appliances.ToIntegerString());
                });
                option.AddPolicy(PolicyName.AdminOrUser, config =>
                {
                    config.RequireRole(UserType.Admin.ToIntegerString(), UserType.User.ToIntegerString());
                });
            });
        }
    }
}
