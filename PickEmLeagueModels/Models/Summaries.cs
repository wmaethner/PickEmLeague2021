using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueModels.Models
{
    public class WeekSummary : IBaseSummary
    {
        public WeekSummary(User user, int week)
        {
            User = user;
            Week = week;
            PickStatus = WeekPickStatus.NotPicked;
        }

        public User User { get; private set; }
        public int Score { get; set; }
        public int CorrectPicks { get; set; }
        public int Place { get; set; }

        public int Week { get; private set; }
        public WeekPickStatus PickStatus { get; set; }
    }

    public class SeasonSummary : IBaseSummary
    {
        public SeasonSummary(User user)
        {
            User = user;
        }

        public User User { get; private set; }
        public int Score { get; set; }
        public int CorrectPicks { get; set; }
        public int Place { get; set; }
    }

    public interface IBaseSummary
    {
        public User User { get; }
        public int Score { get; set; }
        public int CorrectPicks { get; set; }
        public int Place { get; set; }
    }
}
