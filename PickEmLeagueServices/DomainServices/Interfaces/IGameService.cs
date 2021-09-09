using System;
using System.Threading.Tasks;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IGameService
    {
        Task<bool> DeleteGame(long id);
    }
}
