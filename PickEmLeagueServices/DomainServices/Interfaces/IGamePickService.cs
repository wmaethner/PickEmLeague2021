using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IGamePickService
    {
        IEnumerable<GamePick> GetByUser(long userId);
        IEnumerable<GamePick> GetByWeek(int week);
        Task<IEnumerable<GamePick>> GetByUserAndWeekAsync(long userId, int week);
        Task<bool> UpdateByUserAndWeekAsync(IEnumerable<GamePick> gamePicks);
    }
}
