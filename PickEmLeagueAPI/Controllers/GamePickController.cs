using System;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueServices.Services;

namespace PickEmLeagueAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GamePickController : CoreController<GamePick, PickEmLeagueDatabase.Entities.GamePick, IGamePickService>
    {
        IGamePickService _gamePickService;

        public GamePickController(IGamePickService gamePickService) : base(gamePickService)
        {
            _gamePickService = gamePickService;
        }
    }
}
