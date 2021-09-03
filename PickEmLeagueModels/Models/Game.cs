using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueModels.Models
{
    public class Game : DbModel
    {
        public int HomeTeam { get; set; }
        public int AwayTeam { get; set; }
        public int Week { get; set; }
        public DateTime GameTime { get; set; }
        public GameResult GameResult { get; set; }
    }
}
