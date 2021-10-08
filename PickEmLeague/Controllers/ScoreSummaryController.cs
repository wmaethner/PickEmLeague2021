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

        [HttpGet("getWeekWinner")]
        public WeekWinnerResponse GetWeekWinner(int week)
        {
            var winner = _scoreSummaryService.GetWeekWinner(week);
            return new WeekWinnerResponse()
            {
                Winner = winner,
                FoundWinner = winner != null
            };
        }

        [HttpGet("getWeekSummaries")]
        public List<WeekSummary> GetWeekSummaries(int week)
        {
            return _scoreSummaryService.GetWeekSummaries(week);
        }

        [HttpGet("getSeasonSummaries")]
        public List<SeasonSummary> GetSeasonSummaries()
        {
            return _scoreSummaryService.GetSeasonSummaries();
        }
    }
}
