using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Implementations
{
    public class GamePickRepository : IGamePickRepository
    {
        private readonly PickEmLeagueDbContext _dbContext;
        private readonly IMapper _mapper;

        public GamePickRepository(PickEmLeagueDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GamePick> CreateAsync(Game game, User user)
        {
            GamePick gamePick = new GamePick()
            {
                Game = game,
                GameId = game.Id,
                User = user,
                UserId = user.Id
            };
            await _dbContext.AddAsync(gamePick);
            await _dbContext.SaveChangesAsync();
            return gamePick;
        }

        public async Task<GamePick> GetAsync(long id)
        {
            return await GetQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }

        public IEnumerable<GamePick> GetByUser(long userId)
        {
            return GetQueryable()
                .Where(g => g.UserId == userId)
                .ToList();
        }

        public IEnumerable<GamePick> GetByUserAndWeek(long userId, int week)
        {
            var query = GetQueryable();

            return query
                .Where(g => g.UserId == userId)
                .Where(g => g.Game.Week == week)
                .ToList();
        }

        public IQueryable<GamePick> GetQueryable()
        {
            return _dbContext.Set<GamePick>()
                .AsQueryable()
                .Include(gp => gp.Game);
        }

        public async Task<GamePick> UpdateAsync(PickEmLeagueModels.Models.GamePick gamePick)
        {
            var entity = await GetAsync(gamePick.Id);
            _mapper.Map(gamePick, entity);
            var result = _dbContext.SaveChanges();
            return entity;
        }
    }
}
