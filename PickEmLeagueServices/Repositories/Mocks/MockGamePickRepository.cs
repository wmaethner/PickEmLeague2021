using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Mocks
{
    public class MockGamePickRepository : IGamePickRepository
    {
        private readonly IMapper _mapper;
        private long _currentId = 1;
        private Dictionary<long, GamePick> _gamePicks = new Dictionary<long, GamePick>();
        private IGameRepository _gameRepository;

        public MockGamePickRepository(IMapper mapper, IGameRepository gameRepository)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public void ClearDb()
        {
            _gamePicks.Clear();
        }

        public Task<GamePick> CreateAsync(Game game, User user)
        {
            GamePick gamePick = new GamePick()
            {
                Id = _currentId++,
                Game = game,
                GameId = game.Id,
                User = user,
                UserId = user.Id
            };
            _gamePicks.Add(gamePick.Id, gamePick);

            return Task.FromResult(gamePick);
        }

        public Task<GamePick> GetAsync(long id)
        {
            return Task.FromResult(_gamePicks[id]);
        }

        public IEnumerable<GamePick> GetByUser(long userId)
        {
            return GetQueryable()
                .Where(g => g.UserId == userId)
                .ToList();
        }

        public IEnumerable<GamePick> GetByUserAndWeek(long userId, int week)
        {
            return GetQueryable()
                .Where(g => g.UserId == userId)
                .Where(g => g.Game.Week == week)
                .ToList();
        }

        public IEnumerable<GamePick> GetByWeek(int week)
        {
            return GetQueryable()
                .Where(g => g.Game.Week == week)
                .ToList();
        }

        public async Task<GamePick> UpdateAsync(PickEmLeagueModels.Models.GamePick gamePick)
        {
            var entity = await GetAsync(gamePick.Id);
            _mapper.Map(gamePick, entity);
            _gamePicks[entity.Id] = entity;
            return entity;
        }

        private IQueryable<GamePick> GetQueryable()
        {
            var games = _gameRepository.GetAll();

            // Only return game picks where the games still exist
            // This is automatically handled with EF Core
            return _gamePicks.Values
                .Where(gp => games.Where(g => g.Id == gp.Game.Id)
                                  .Any())
                .AsQueryable();
        }
    }
}
