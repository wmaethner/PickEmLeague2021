using System;
using System.Collections.Generic;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface IGameRepository : ICrudRepository<Game, PickEmLeagueModels.Models.Game>
    {
        IEnumerable<Game> GetByWeek(int week);
        void ClearDb();
    }
}
