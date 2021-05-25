using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueServices.Interfaces
{
    public interface IGamePickService : IBaseService<GamePick, PickEmLeagueDatabase.Entities.GamePick>
    {
        Task<IEnumerable<GamePick>> GetUsersGamePicks(User user);
        Task<IEnumerable<GamePick>> GetUsersGamePicks(Guid userId);

        Task<IEnumerable<GamePick>> GetUsersWeekGamePicks(User user, int week);
        Task<IEnumerable<GamePick>> GetUsersWeekGamePicks(Guid userId, int week);
    }
}
