using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PickEmLeague.Global.Shared;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Databases;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDatabase.Repositories;
using PickEmLeagueServices;
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
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
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
            List<Type> scopedServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => x.GetCustomAttribute<DIServiceScopeAttribute>() != null)
                .ToList();

            foreach (Type type in scopedServices)
            {
                DIServiceScopeAttribute diServiceScope = type.GetCustomAttribute<DIServiceScopeAttribute>();

                switch (diServiceScope.ServiceScope)
                {
                    case ServiceScope.Scoped:
                        services.AddScoped(diServiceScope.InterfaceType, diServiceScope.ImplementationType);
                        break;
                    case ServiceScope.Transient:
                        services.AddTransient(diServiceScope.InterfaceType, diServiceScope.ImplementationType);
                        break;
                    case ServiceScope.Singleton:
                        services.AddSingleton(diServiceScope.InterfaceType, diServiceScope.ImplementationType);
                        break;
                }
            }      
        }

        private static void AddDependencies(IServiceCollection services)
        {
            ApiVersion version = new(1, 0);

            services.AddAutoMapper(Assembly.GetAssembly(typeof(PickEmLeagueIOC.Profiles.UserProfile)));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(PickEmLeagueIOC.Profiles.GameProfile)));

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
