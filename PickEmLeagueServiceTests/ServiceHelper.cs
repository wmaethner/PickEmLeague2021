using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeague.Registrations;
using PickEmLeagueModels.Profiles;

namespace PickEmLeagueServiceTests
{
    public static class ServiceHelper
    {
        public static IServiceProvider BuildUnitTestServices(string testDatabase = "testDb")
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.test.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var servicesCollection = new ServiceCollection();
            servicesCollection.RegisterRepositories();
            servicesCollection.RegisterServices();
            servicesCollection.RegisterDatabase(configuration, testDatabase);
            servicesCollection.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));

            return servicesCollection.BuildServiceProvider();
        }
    }
}
