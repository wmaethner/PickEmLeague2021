using PickEmLeague.Global.Shared;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    [DIServiceScope(typeof(IUserRepository), typeof(UserRepository))]
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }   
    }
}
