using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GameController : CoreController<Game, PickEmLeagueDatabase.Entities.Game, IGameService>
    {
        IGameService _gameService;

        public GameController(IGameService gameService) : base(gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("byweek/{week}")]
        public async Task<IEnumerable<Game>> GetGamesByWeek([FromRoute] int week)
        {
            return await _gameService.GetWeeksGames(week);
        }
    }
}
