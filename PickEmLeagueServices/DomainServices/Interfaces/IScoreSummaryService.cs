using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IScoreSummaryService
    {
        IEnumerable<UserSummary> GetSummaries(int week);
        User? GetWeekWinner(int week);
    }
}
