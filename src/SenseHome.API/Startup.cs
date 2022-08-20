using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SenseHome.API.Configuration;

namespace SenseHome.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwtAuthentication(Configuration);
            services.AddSenseHomeAuthorization();
            services.AddControllers();
            services.AddDataContext(Configuration);
            services.AddAutoMapper();
            services.AddDependentServicesAndRepositories();
            services.AddSwaggerService();
            services.AddConfiguredCors(Configuration);
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSenseHomeExceptionHandler(env.IsDevelopment());
            app.UseRouting();
            app.UseAuthentication();
            app.UseConfiguredCors();
            app.UseAuthorization();
            app.UseSwaggerMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
