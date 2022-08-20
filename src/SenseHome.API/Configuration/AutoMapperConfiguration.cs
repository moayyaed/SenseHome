using Microsoft.Extensions.DependencyInjection;
using SenseHome.Services.Mapper;

namespace SenseHome.API.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperBuilder = new MapperBuilder();
            AutoMapper.IMapper mapper = mapperBuilder.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
