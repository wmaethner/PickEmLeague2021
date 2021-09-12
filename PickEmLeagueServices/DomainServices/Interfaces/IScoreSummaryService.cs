using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IScoreSummaryService
    {
        Task<IEnumerable<UserSummary>> GetSummariesAsync(int week);
    }
}
