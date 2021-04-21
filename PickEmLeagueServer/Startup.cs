using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PickEmLeagueAPI.Utilities;
using PickEmLeagueDatabase;
using PickEmLeagueServer.Utilities;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueServices.Services;
using PickEmLeagueUtils;

namespace PickEmLeagueServer
{
    public class Startup
    {
        private readonly ApiVersion _version = new(1, 0);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            //services.AddDbContextPool<DatabaseContext>(options =>
            //    {
            //        options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            //        options.UseSnakeCaseNamingConvention();
            //    });

            services.AddAPIServices(Configuration);
            //AddDependencies(services);
            //AddServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v{_version}/swagger.json", $"PickEmLeague API v{_version}");
                });
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        //private void AddServices(IServiceCollection services)
        //{
        //    services.AddScoped<IUserService, UserService>();
        //}

        //private void AddDependencies(IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(Startup));

        //    services.AddApiVersioning(c =>
        //    {
        //        c.AssumeDefaultVersionWhenUnspecified = true;
        //        c.DefaultApiVersion = _version;
        //    });

        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc($"v{_version}",
        //            new OpenApiInfo { Title = "PickEmLeague API", Version = $"v{_version}" });
        //        c.OperationFilter<RemoveVersionFromParameter>();

        //        c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
        //    });
        //}
    }
}
