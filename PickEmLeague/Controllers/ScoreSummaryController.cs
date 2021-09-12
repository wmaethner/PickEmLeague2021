using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueModels.Models;
using PickEmLeagueModels.Models.Responses;
using PickEmLeagueServices.DomainServices.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreSummaryController : ControllerBase
    {
        private readonly IScoreSummaryService _scoreSummaryService;
        private readonly ILogger _logger;

        public ScoreSummaryController(IScoreSummaryService scoreSummaryService, ILogger<ScoreSummaryController> logger)
        {
            _scoreSummaryService = scoreSummaryService;
            _logger = logger;
        }

        [HttpGet("getScoreSummaries")]
        public async Task<IEnumerable<UserSummary>> GetScoreSummaryAsync(int week)
        {
            _logger.LogInformation($"Get score summaries: {week}");
            return await _scoreSummaryService.GetSummariesAsync(week);
        }
    }
}
