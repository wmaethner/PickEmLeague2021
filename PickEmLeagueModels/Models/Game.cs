using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueModels.Models
{
    public class Game : DbModel
    {
        public int Week { get; set; }
        public DateTime GameTime { get; set; }
        public string GameTimeString { get; set; }
        public string GameIsoString { get; set; }
        public bool HasStarted { get; set; }

        public GameResult GameResult { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public long HomeTeamId { get; set; } = 1;
        public long AwayTeamId { get; set; } = 2;
    }
}
