using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase.Models;

namespace PickEmLeagueServer.Database
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }
}
