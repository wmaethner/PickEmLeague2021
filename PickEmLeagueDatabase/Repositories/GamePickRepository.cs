using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeague.Global.Shared;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    [DIServiceScope(typeof(IGamePickRepository), typeof(GamePickRepository))]
    public class GamePickRepository : BaseRepository<GamePick>, IGamePickRepository
    {
        public GamePickRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<GamePick>> GetUsersGamePicks(User user)
        {
            return await GetUsersGamePicks(user.Id);
        }

        public async Task<IEnumerable<GamePick>> GetUsersGamePicks(Guid userId)
        {
            return (await GetAll()).ToList().Where(g => g.UserId == userId);
        }
    }
}
