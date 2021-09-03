using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueDatabase.Entities
{
    public class Game : DbEntity
    {
        public int HomeTeam { get; set; }
        public int AwayTeam { get; set; }
        public int Week { get; set; }
        public DateTime GameTime { get; set; }
        public GameResult GameResult { get; set; }
    }
}
