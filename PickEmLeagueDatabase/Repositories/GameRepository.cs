using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeague.Global.Shared;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    [DIServiceScope(typeof(IGameRepository), typeof(GameRepository))]
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<Game>> GetWeeksGames(int week)
        {
            return (await GetAll()).ToList().Where(g => g.Week == week);
        }
    }
}
