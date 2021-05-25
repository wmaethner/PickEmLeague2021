using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeague.Global.Shared.Enums;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueUtils;
using Xunit;

namespace PickEmLeagueServerTests.ServiceTests
{
    public class GameServiceTests : BaseServiceTests<Game, PickEmLeagueDatabase.Entities.Game>
    {
        private readonly IGameService _gameService;

        public GameServiceTests()
        {
            var services = ServiceUtils.BuildTestServiceProvider();
            _gameService = services.GetRequiredService<IGameService>();
        }

        [Fact]
        public async Task GetWeeksGamesSucceeds()
        {
            await AddModel(MakeGame(1));
            await AddModel(MakeGame(1));
            await AddModel(MakeGame(2));
            await AddModel(MakeGame(3));
            await AddModel(MakeGame(3));
            await AddModel(MakeGame(3));

            List<Game> games = await GetAllModels();

            Assert.Equal(2, (await _gameService.GetWeeksGames(1)).Count());
            Assert.Equal(1, (await _gameService.GetWeeksGames(2)).Count());
            Assert.Equal(3, (await _gameService.GetWeeksGames(3)).Count());
        }

        public override Game NewModel()
        {
            return MakeGame(1);
        }

        public override Game UpdateModel(Game model)
        {
            model.AwayTeamId++;
            return model;
        }

        protected override IBaseService<Game, PickEmLeagueDatabase.Entities.Game> BaseService()
        {
            return _gameService;
        }

        private Game MakeGame(int week)
        {
            return new Game()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Week = week,
                Result = GameResultEnum.HomeWin,
                GameTime = new DateTime()
            };
        }
    }
}
