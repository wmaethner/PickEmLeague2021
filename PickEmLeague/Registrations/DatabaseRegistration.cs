using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase;

namespace PickEmLeague.Registrations
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services,
            IConfiguration configuration, string testDatabse = null)
        {
            if (!string.IsNullOrEmpty(testDatabse))
            {
                services.AddDbContext<PickEmLeagueDbContext>(opts =>
                    opts.UseInMemoryDatabase(testDatabse)
                );
            }
            else
            {
                services.AddDbContext<PickEmLeagueDbContext>(opts =>
                {
                    opts.UseNpgsql(configuration.GetConnectionString("PostgresDbConnection"),
                        b => b.MigrationsAssembly(
                            Assembly.GetAssembly(typeof(PickEmLeagueDbContext)).GetName().FullName));
                });
            }

            return services;
        }
    }
}
