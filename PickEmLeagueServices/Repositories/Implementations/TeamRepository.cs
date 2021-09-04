using System.Collections.Generic;
using System.Linq;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        private readonly PickEmLeagueDbContext _dbContext;

        public TeamRepository(PickEmLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Team> GetAll()
        {
            return _dbContext.Set<Team>().AsQueryable().ToList().OrderBy(t => t.Id);
        }
    }
}
