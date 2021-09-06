using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamePickController : ControllerBase
    {
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GamePickController(IGamePickRepository gamePickRepository, IMapper mapper, ILogger logger)
        {
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
            return _mapper.Map<IEnumerable<GamePick>>(
                _gamePickRepository.GetByUser(userId));
        }

        [HttpGet("getGamePickByUserAndWeek")]
        public IEnumerable<GamePick> GetByUserAndWeek(long userId, int week)
        {
            return _mapper.Map<IEnumerable<GamePick>>(
                _gamePickRepository.GetByUserAndWeek(userId, week));
        }
    }
}
