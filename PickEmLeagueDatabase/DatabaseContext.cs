using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase.Models;

namespace PickEmLeagueDatabase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
