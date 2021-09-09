using System;
using System.Collections.Generic;

namespace PickEmLeagueModels.Models.Responses
{
    public class ScoreSummaryResponse
    {
        public int Week { get; set; }
        public List<UsersWeeklyScoreSummary> UsersWeeklyScoreSummaries { get; set; }
    }

    public class UsersWeeklyScoreSummary
    {
        public User User { get; set; }
        public int WeekScore { get; set; }
        public int SeasonScore { get; set; }
    }
}
