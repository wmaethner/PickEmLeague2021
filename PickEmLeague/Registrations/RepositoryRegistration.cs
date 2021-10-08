using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueServices.Repositories.Implementations;
using PickEmLeagueServices.Repositories.Interfaces;
using PickEmLeagueServices.Repositories.Mocks;

namespace PickEmLeague.Registrations
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IGamePickRepository, GamePickRepository>();
            services.AddScoped<IMiscAdminRepository, MiscAdminRepository>();

            return services;
        }

        public static IServiceCollection RegisterMockRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, MockUserRepository>();
            services.AddScoped<IGameRepository, MockGameRepository>();
            services.AddScoped<ITeamRepository, MockTeamRepository>();
            services.AddScoped<IGamePickRepository, MockGamePickRepository>();
            //services.AddScoped<IMiscAdminRepository, MiscAdminRepository>();

            return services;
        }
    }
}
