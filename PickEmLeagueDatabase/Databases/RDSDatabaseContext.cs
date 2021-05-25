using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase
{
    public class RDSDatabaseContext : DbContext, IDatabaseContext
    {
        //public DbSet<User> Users { get; set; }

        public RDSDatabaseContext(DbContextOptions<RDSDatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Id);
            modelBuilder.Entity<Game>().HasIndex(g => g.Id);
            modelBuilder.Entity<GamePick>().HasIndex(g => g.Id);
        }

        public async Task Delete<T>(object key) where T : class
        {
            var entity = Get<T>(key);
            Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public Task<IQueryable<T>> GetQueryableAsync<T>() where T : class
        {
            return Task.Run(() => Set<T>().AsQueryable());
        }

        public T Get<T>(object key) where T : class
        {
            return Set<T>().Find(key);
        }

        public async Task Create<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }       
    }
}
