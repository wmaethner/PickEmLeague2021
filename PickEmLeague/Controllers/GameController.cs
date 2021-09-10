using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    public class GameController : CrudController<PickEmLeagueDatabase.Entities.Game, Game>
    {
        private readonly IGameService _gameService;

        public GameController(IGameRepository repository, IMapper mapper,
            IGameService gameService, ILogger<GameController> logger) :
            base(repository, mapper, logger)
        {
            _gameService = gameService;
        }

        [HttpPost("create-game-for-week")]
        public async Task<Game> Create(int week)
        {
            return await _gameService.CreateForWeek(week);
        }

        [HttpGet("get-games-for-week")]
        public IEnumerable<Game> GetForWeek(int week)
        {
            return _gameService.GetForWeek(week);
        }

        [HttpDelete("delete-game")]
        public new async Task Delete(long id)
        {
            await _gameService.DeleteGame(id);
        }

        [HttpPost("add-game-schedule")]
        public async Task AddScheduleAsync(IFormFile csvFile)
        {
            if (csvFile.FileName.EndsWith(".csv"))
            {
                using (var stream = new StreamReader(csvFile.OpenReadStream()))
                {
                    await _gameService.AddScheduleAsync(stream);
                }
            }
        }

    }
}
