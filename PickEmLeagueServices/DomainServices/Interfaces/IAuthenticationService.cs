using System;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        User AttempLogin(string email, string passwordHash);
    }
}
