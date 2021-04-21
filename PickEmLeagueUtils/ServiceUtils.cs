using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PickEmLeagueDatabase;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueServices.Services;
using static PickEmLeagueDatabase.DBContextFactory;

namespace PickEmLeagueUtils
{
    public static class ServiceUtils
    {

        public class APIServiceOptions
        {
            public bool UseInMemoryTestDatabase { get; set; } = false;
        }

        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration,
            Action<APIServiceOptions>? buildAPIOptions = null)
        {
            var apiOptions = new APIServiceOptions();
            buildAPIOptions?.Invoke(apiOptions);

            services.AddDbContextPool<DatabaseContext>(options =>
            {
                DbContextFactory.ConfigureOptionsBuilder(options, configuration, apiOptions.UseInMemoryTestDatabase);
            });

            ServiceUtils.AddDependencies(services);
            ServiceUtils.AddServices(services);

            return services;
        }

        public static IServiceProvider BuildTestServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.test.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection()
                .AddAPIServices(configuration, opts =>
                {
                    opts.UseInMemoryTestDatabase = true;
                })
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();


            var db = services.GetRequiredService<DatabaseContext>();
            db.Database.EnsureCreated();

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        private static void AddDependencies(IServiceCollection services)
        {
            ApiVersion version = new(1, 0);

            services.AddAutoMapper(Assembly.GetAssembly(typeof(PickEmLeagueIOC.Profiles.UserProfile)));

            services.AddApiVersioning(c =>
            {
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.DefaultApiVersion = version;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{version}",
                    new OpenApiInfo { Title = "PickEmLeague API", Version = $"v{version}" });
                c.OperationFilter<RemoveVersionFromParameter>();

                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });
        }
    }
}
