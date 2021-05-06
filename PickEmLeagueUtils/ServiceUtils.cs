using System;
using System.Reflection;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Databases;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDatabase.Repositories;
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

            //ServiceUtils.AddRDSDatabaseContext(services, configuration, buildAPIOptions);

            services.AddAWSService<IAmazonDynamoDB>();

            ServiceUtils.AddDatabase(services, configuration, apiOptions);
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

            var db = services.GetRequiredService<IDatabaseContext>() as RDSDatabaseContext;
            db.Database.EnsureCreated();

            return services;
        }

        private static void AddRDSDatabaseContext(IServiceCollection services, IConfiguration configuration,
            Action<APIServiceOptions>? buildAPIOptions = null)
        {
            var apiOptions = new APIServiceOptions();
            buildAPIOptions?.Invoke(apiOptions);

            services.AddDbContextPool<IDatabaseContext, RDSDatabaseContext>(options =>
            {
                DbContextFactory.ConfigureOptionsBuilder(options, configuration, apiOptions.UseInMemoryTestDatabase);
            });
        }

        private static void AddDatabase(IServiceCollection services, IConfiguration configuration, APIServiceOptions buildAPIOptions)
        {
            string dbString = configuration.GetValue<string>("Database");

            if (buildAPIOptions.UseInMemoryTestDatabase || dbString == "RDS")
            {
                services.AddDbContextPool<IDatabaseContext, RDSDatabaseContext>(options =>
                {
                    DbContextFactory.ConfigureOptionsBuilder(options, configuration, buildAPIOptions.UseInMemoryTestDatabase);
                });
            }
            else if (dbString == "DynamoDB")
            {
                services.AddScoped<IDatabaseContext, DynamoDBDatabaseContext>();
            }
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRepository, UserRepository>();
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
