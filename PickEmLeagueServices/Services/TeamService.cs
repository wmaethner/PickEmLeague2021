using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PickEmLeague.Global.Shared;
using PickEmLeague.Global.StaticData;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    [DIServiceScope(typeof(ITeamService), typeof(TeamService), ServiceScope.Singleton)]
    public class TeamService : ITeamService
    {
        Dictionary<int, Team> _teams;
        public TeamService()
        {
            string data = FileReader.ReadFile("Teams");
            _teams = JsonConvert.DeserializeObject<Dictionary<int, Team>>(data);
        }

        public Team GetTeam(int id)
        {
            if (!_teams.ContainsKey(id))
            {
                throw new ArgumentOutOfRangeException();
            }
            return _teams[id];
        }

        public List<Team> GetTeams()
        {
            return _teams.Select(x => x.Value).ToList();
        }
    }
}
