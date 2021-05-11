using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueServices.Interfaces
{
    public interface IGameService : IBaseService<Game, PickEmLeagueDatabase.Entities.Game>
    {
        Task<IEnumerable<Game>> GetWeeksGames(int week);
    }
}
