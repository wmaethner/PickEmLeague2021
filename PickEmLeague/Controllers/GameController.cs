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
    public class GameController : CrudController<PickEmLeagueDatabase.Entities.Game, Game>
    {
        private readonly IGameService _gameService;

        public GameController(IGameRepository repository, IMapper mapper,
            IGameService gameService, ILogger<GameController> logger) :
            base(repository, mapper, logger)
        {
            _gameService = gameService;
        }

        [HttpDelete("delete-game")]
        public new async Task Delete(long id)
        {
            //_logger.LogDebug($"Deleting {typeof(TModel)} with id {id}");
            //await _repository.DeleteAsync(id);
            await _gameService.DeleteGame(id);
        }
    }
}
