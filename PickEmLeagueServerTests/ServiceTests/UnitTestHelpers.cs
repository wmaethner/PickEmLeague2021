using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase;

namespace PickEmLeagueServerTests.ServiceTests
{
    public static class UnitTestHelpers
    {
        //public static IServiceProvider BuildTestServiceProvider()
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", true, true)
        //        .AddJsonFile("appsettings.test.json", true, true)
        //        .AddEnvironmentVariables()
        //        .Build();

        //    var services = new ServiceCollection()
        //        .AddEnrollment(configuration, opts =>
        //        {
        //            opts.UseInMemoryTestDatabase = true;
        //            opts.UseMockIntegrations = true;
        //        })
        //        .AddSingleton<IConfiguration>(configuration)
        //        .BuildServiceProvider();


        //    var db = services.GetRequiredService<DatabaseContext>();
        //    db.Database.EnsureCreated();

        //    return services;
        //}
    }
}
