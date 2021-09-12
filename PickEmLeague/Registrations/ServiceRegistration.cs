using System;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueServices.DomainServices.Implementations;
using PickEmLeagueServices.DomainServices.Interfaces;

namespace PickEmLeague.Registrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGamePickService, GamePickService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IScoreSummaryService, ScoreSummaryService>();
            services.AddScoped<IAwsS3Service, AwsS3Service>();

            return services;
        }
    }
}
