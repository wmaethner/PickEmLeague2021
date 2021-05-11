using System;
namespace PickEmLeagueDomain.Models
{
    public class Game : GuidBasedModel
    {
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? Winner { get; set; }
        public DateTime GameTime { get; set; }
    }
}
