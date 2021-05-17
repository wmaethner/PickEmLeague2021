using System;
using PickEmLeague.Global.Shared.Enums;

namespace PickEmLeagueDomain.Models
{
    public class Game : GuidBasedModel
    {
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? Winner { get; set; }
        public GameResultEnum Result { get; set; }
        public DateTime GameTime { get; set; }
    }
}
