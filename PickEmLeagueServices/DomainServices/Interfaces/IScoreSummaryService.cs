using System;
using System.Collections.Generic;
using PickEmLeagueModels.Models.Responses;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IScoreSummaryService
    {
        IEnumerable<UsersWeeklyScoreSummary> GetScoreSummary(int week);
    }
}
