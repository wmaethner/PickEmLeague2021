using AutoMapper;
using Microsoft.Extensions.Logging;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    public class GameController : CrudController<PickEmLeagueDatabase.Entities.Game, Game>
    {
        public GameController(IGameRepository repository, IMapper mapper, ILogger<GameController> logger) :
            base(repository, mapper, logger)
        {
        }
    }
}
