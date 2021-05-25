using System;
using PickEmLeague.Global.Shared.Enums;

namespace PickEmLeagueDomain.Models
{
    public class Game : GuidBasedModel
    {
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public GameResultEnum Result { get; set; }
        public DateTime GameTime { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Game);
        }

        public bool Equals(Game other)
        {
            return other != null &&
                   Week == other.Week &&
                   HomeTeamId == other.HomeTeamId &&
                   AwayTeamId == other.AwayTeamId &&
                   Result == other.Result &&
                   GameTime == other.GameTime;                
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Week, HomeTeamId, AwayTeamId, Result, GameTime);
        }
    }
}
