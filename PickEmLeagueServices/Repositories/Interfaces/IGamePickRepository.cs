using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface IGamePickRepository
    {
        Task<GamePick> CreateAsync(Game game, User user);
        Task<GamePick> GetAsync(long id);
        IEnumerable<GamePick> GetByUser(long userId);
        IEnumerable<GamePick> GetByWeek(int week);
        IEnumerable<GamePick> GetByUserAndWeek(long userId, int week);
        Task<GamePick> UpdateAsync(PickEmLeagueModels.Models.GamePick gamePick);
    }
}
