using System;
using System.Data.Common;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PickEmLeagueDatabase
{
    public class DBContextFactory
    {
        public class DbContextFactory : IDesignTimeDbContextFactory<RDSDatabaseContext>
        {
            /// <inheritdoc />
            public RDSDatabaseContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<RDSDatabaseContext>();
                ConfigureOptionsBuilder(optionsBuilder, configuration);
                return new RDSDatabaseContext(optionsBuilder.Options);
            }

            public static void ConfigureOptionsBuilder(DbContextOptionsBuilder optionsBuilder,
                IConfiguration configuration, bool useInMemoryDb = false)
            {
                var currentAssembly = Assembly.GetAssembly(typeof(DbContextFactory))!;
                var assemblyName = currentAssembly.GetName().FullName;

                if (useInMemoryDb)
                    optionsBuilder.UseSqlite(CreateInMemoryDatabase());
                else
                    optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                    //optionsBuilder.UseMySql(configuration.GetConnectionString("DynamoDBConnection"));

                
                        //b => { b.MigrationsAssembly(assemblyName); });

                optionsBuilder.UseSnakeCaseNamingConvention();
            }

            private static DbConnection CreateInMemoryDatabase()
            {
                var connection = new SqliteConnection("Filename=:memory:");

                connection.Open();

                return connection;
            }
        }
    }
}
