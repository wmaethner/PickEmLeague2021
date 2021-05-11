using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }   
    }
}
