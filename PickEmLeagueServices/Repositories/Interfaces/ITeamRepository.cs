using System;
using System.Collections.Generic;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetAll();
    }
}
