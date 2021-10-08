using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IScoreSummaryService
    {
        List<WeekSummary> GetWeekSummaries(int week);
        List<SeasonSummary> GetSeasonSummaries();

        User? GetWeekWinner(int week);
    }
}
