using System;
using System.Collections.Generic;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueServices.Interfaces
{
    public interface ITeamService
    {
        Team GetTeam(int id);
        List<Team> GetTeams();
    }
}
