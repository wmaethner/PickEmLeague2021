using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueModels.Models.Responses;
using PickEmLeagueServices.DomainServices.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreSummaryController : ControllerBase
    {
        private readonly IScoreSummaryService _scoreSummaryService;

        public ScoreSummaryController(IScoreSummaryService scoreSummaryService)
        {
            _scoreSummaryService = scoreSummaryService;
        }

        [HttpGet("getScoreSummary")]
        public IEnumerable<UsersWeeklyScoreSummary> GetScoreSummary(int week)
        {
            return _scoreSummaryService.GetScoreSummary(week);
        }
    }
}
