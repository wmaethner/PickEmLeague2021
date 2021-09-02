using System;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueDatabase
{
    public class PickEmLeagueDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        public PickEmLeagueDbContext(DbContextOptions<PickEmLeagueDbContext> options) : base(options)
        {
        }
    }
}

