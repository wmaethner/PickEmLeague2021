using System;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueServer.Models;

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
