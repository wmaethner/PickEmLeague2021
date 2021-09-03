using System;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface IGameRepository : ICrudRepository<Game, PickEmLeagueModels.Models.Game>
    {
    }
}
