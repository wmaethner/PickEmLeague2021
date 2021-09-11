using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueModels.Models
{
    public class UserSummary
    {
        public User User { get; set; }
        public UserWeekSummary WeekSummary { get; set; }
        public UserSeasonSummary SeasonSummary { get; set; }

        public UserSummary()
        {
            WeekSummary = new UserWeekSummary();
            SeasonSummary = new UserSeasonSummary();
        }
    }

    public class UserWeekSummary
    {
        public int WeekScore { get; set; }
        public WeekPickStatus WeekPickStatus { get; set; }
        public int CorrectPicks { get; set; }
    }

    public class UserSeasonSummary
    {
        public int SeasonScore { get; set; }
        public int CorrectPicks { get; set; }
    }
}
