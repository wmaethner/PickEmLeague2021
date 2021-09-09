using PickEmLeagueModels.Models.Responses;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        LoginResponse AttempLogin(string email, string passwordHash);
    }
}
