using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Filters;
using RugbyUnion.ManagementSystem.Services;
using System.Net.Mime;

namespace RugbyUnion.ManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers
            services.AddControllers(options => options.Filters.Add<ValidateModelActionFilter>())
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);
                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        return result;
                    };
                });

            // Database
            services.AddDbContext<RugbyUnionContext>(opt => opt.UseInMemoryDatabase("RubgyUnionManagementDatabase").UseLazyLoadingProxies());

            // Services
            services.AddTransient<ICrudService, CrudService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IStadiumService, StadiumService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
