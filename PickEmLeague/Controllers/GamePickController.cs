using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamePickController : ControllerBase
    {
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IGamePickService _gamePickService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GamePickController(IGamePickService gamePickService, IGamePickRepository gamePickRepository, IMapper mapper, ILogger<GamePickController> logger)
        {
            _gamePickService = gamePickService;
            _gamePickRepository = gamePickRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("getGamePick")]
        public async Task<GamePick> Get(long id)
        {
            _logger.LogDebug($"Getting GamePick with id {id}");
            return _mapper.Map<GamePick>(await _gamePickRepository.GetAsync(id));
        }

        [HttpGet("getGamePickByUser")]
        public IEnumerable<GamePick> GetByUser(long userId)
        {
            return _gamePickService.GetByUser(userId);
        }

        [HttpGet("getGamePickByWeek")]
        public IEnumerable<GamePick> GetByWeek(int week)
        {
            return _gamePickService.GetByWeek(week);
        }

        [HttpGet("getGamePicksByUserAndWeek")]
        public async Task<IEnumerable<GamePick>> GetByUserAndWeekAsync(long userId, int week)
        {
            return await _gamePickService.GetByUserAndWeekAsync(userId, week);
        }

        [HttpPost("updateGamePicks")]
        public async Task<bool> UpdateGamePicksAsync(IEnumerable<GamePick> gamePicks)
        {
            await _gamePickService.UpdateByUserAndWeekAsync(gamePicks);
            return true;
        }
    }
}
