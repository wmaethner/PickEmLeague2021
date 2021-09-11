using System.Collections.Generic;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IScoreSummaryService
    {
        IEnumerable<UserSummary> GetSummaries(int week);
    }
}
