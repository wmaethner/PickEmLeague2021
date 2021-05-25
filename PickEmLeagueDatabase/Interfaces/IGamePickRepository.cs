using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IGamePickRepository : IBaseRepository<GamePick>
    {
        Task<IEnumerable<GamePick>> GetUsersGamePicks(User user);
        Task<IEnumerable<GamePick>> GetUsersGamePicks(Guid userId);
    }
}
