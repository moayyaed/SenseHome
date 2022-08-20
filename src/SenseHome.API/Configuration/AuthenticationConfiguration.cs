using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SenseHome.API.Configuration.Model;
using System;


namespace SenseHome.API.Configuration
{
    public static class JwtConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var apiConfiguration = new SenseHomeApiConfiguration();
            configuration.GetSection(nameof(SenseHomeApiConfiguration)).Bind(apiConfiguration);
            services.AddSingleton(apiConfiguration.AuthenticationConfiguration);
            services.AddSingleton(apiConfiguration.InternalCredentialConfiguration);

            var secretInBytes = System.Text.Encoding.ASCII.GetBytes(apiConfiguration.AuthenticationConfiguration.Secret);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretInBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
