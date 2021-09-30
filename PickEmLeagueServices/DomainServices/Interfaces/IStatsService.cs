using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueModels.Models.Stats;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IStatsService
    {
        Task<IEnumerable<AverageScores>> GetAverageScoresAsync();
    }
}
