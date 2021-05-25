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
    public class GamePickServiceTests : BaseServiceTests<GamePick, PickEmLeagueDatabase.Entities.GamePick>
    {
        private readonly IGameService _gameService;
        private readonly IGamePickService _gamePickService;

        public GamePickServiceTests()
        {
            var services = ServiceUtils.BuildTestServiceProvider();
            _gamePickService = services.GetRequiredService<IGamePickService>();
            _gameService = services.GetRequiredService<IGameService>();
        }


        [Fact]
        public async Task GetUsersGamePicksSucceeds()
        {
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();

            await AddModel(MakeGamePick(Guid.NewGuid(), user1, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user1, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user1, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));

            Assert.Equal(3, (await _gamePickService.GetUsersGamePicks(user1)).Count());
            Assert.Equal(4, (await _gamePickService.GetUsersGamePicks(user2)).Count());
        }

        [Fact]
        public async Task GetUsersWeekGamePicksSucceeds()
        {
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();

            List<Guid> users1Week1Games = new List<Guid>();
            List<Guid> users1Week2Games = new List<Guid>();
            users1Week1Games.Add(await AddGameAsync(1, GameResultEnum.AwayWin));
            users1Week1Games.Add(await AddGameAsync(1, GameResultEnum.AwayWin));
            users1Week1Games.Add(await AddGameAsync(1, GameResultEnum.AwayWin));
            users1Week2Games.Add(await AddGameAsync(2, GameResultEnum.AwayWin));
            users1Week2Games.Add(await AddGameAsync(2, GameResultEnum.AwayWin));
            users1Week2Games.Add(await AddGameAsync(2, GameResultEnum.AwayWin));
            users1Week2Games.Add(await AddGameAsync(2, GameResultEnum.AwayWin));

            foreach (var game in users1Week1Games.Concat(users1Week2Games))
            {
                await AddModel(MakeGamePick(game, user1, GameResultEnum.AwayWin));
            }

            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));
            await AddModel(MakeGamePick(Guid.NewGuid(), user2, GameResultEnum.AwayWin));

            Assert.Equal(users1Week1Games.Count, (await _gamePickService.GetUsersWeekGamePicks(user1, 1)).Count());
            Assert.Equal(users1Week2Games.Count, (await _gamePickService.GetUsersWeekGamePicks(user1, 2)).Count());
        }


        public override GamePick NewModel()
        {
            return MakeGamePick(Guid.NewGuid(), Guid.NewGuid(), GameResultEnum.AwayWin);
        }

        public override GamePick UpdateModel(GamePick model)
        {
            model.Pick = (model.Pick == GameResultEnum.HomeWin) ? GameResultEnum.AwayWin : GameResultEnum.HomeWin;
            return model;
        }

        private GamePick MakeGamePick(Guid gameId, Guid userId, GameResultEnum result)
        {
            return new GamePick()
            {
                Amount = 1,
                Game = gameId,
                Pick = result,
                UserId = userId,
                UserLocked = true
            };
        }

        private async Task<Guid> AddGameAsync(int week, GameResultEnum gameResult)
        {
            Game game = new Game()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Week = week,
                Result = gameResult,
                GameTime = new DateTime()
            };

            return (await _gameService.Add(game)).Id;
        }

        protected override IBaseService<GamePick, PickEmLeagueDatabase.Entities.GamePick> BaseService()
        {
            return _gamePickService;
        }
    }
}
