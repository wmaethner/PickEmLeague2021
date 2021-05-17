using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueUtils;
using Xunit;

namespace PickEmLeagueServerTests.ServiceTests
{
    public class GameServiceTests
    {
        private readonly IGameService _gameService;
        private readonly IDatabaseContext _db;

        public GameServiceTests()
        {
            var services = ServiceUtils.BuildTestServiceProvider();
            _gameService = services.GetRequiredService<IGameService>();
            _db = services.GetService<IDatabaseContext>()!;
        }

        [Fact]
        public async Task GetGameWithGuidSucceeds()
        {
            Game game = await _gameService.Add(new Game()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Winner = 2,
                Week = 1,
                GameTime = new DateTime()
            });

            Game retrievedGame = await _gameService.Get(game.Id);
            Assert.Equal(game.Id, retrievedGame.Id);
            Assert.Equal(game.HomeTeamId, retrievedGame.HomeTeamId);
        }

        [Fact]
        public async Task GetGamesSucceeds()
        {
            Assert.Empty(await _gameService.GetAll());

            await _gameService.Add(new Game()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Winner = 2,
                Week = 1,
                GameTime = new DateTime()
            });
            Assert.Single(await _gameService.GetAll());

            await _gameService.Add(new Game()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Winner = 2,
                Week = 1,
                GameTime = new DateTime()
            });
            Assert.Equal(2, (await _gameService.GetAll()).Count);
        }

        [Fact]
        public async Task UpdateGameSucceeds()
        {
            Game game = await _gameService.Add(new Game()
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                Winner = 2,
                Week = 1,
                GameTime = new DateTime()
            });
            Assert.Equal(game.HomeTeamId, (await _gameService.Get(game.Id)).HomeTeamId);
            game.HomeTeamId = 3;
            await _gameService.Update(game);

            Assert.Single(await _gameService.GetAll());
            Assert.Equal(game.HomeTeamId, (await _gameService.Get(game.Id)).HomeTeamId);
        }
    }
}
