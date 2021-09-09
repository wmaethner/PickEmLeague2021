using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueDatabase.Entities
{
    public class Game : DbEntity
    {
        public int Week { get; set; }
        public DateTime GameTime { get; set; } = DateTime.Now;
        public GameResult GameResult { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public long HomeTeamId { get; set; } = 1;
        public long AwayTeamId { get; set; } = 2;
    }
}
