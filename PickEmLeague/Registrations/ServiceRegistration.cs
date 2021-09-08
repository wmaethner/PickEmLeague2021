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
            services.AddScoped<IGamePickService, GamePickService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
