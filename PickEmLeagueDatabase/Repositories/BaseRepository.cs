using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    public abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : GuidBasedEntity
    {
        private bool _disposed;
        protected IDatabaseContext Context;

        public BaseRepository(IDatabaseContext dbContext)
        {
            Context = dbContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }

        public async Task<T> Add(T entity)
        {
            if ((await Get(entity.Id)) != null)
            {
                throw new Exception($"Entity {typeof(T)} with id {entity.Id} already exists");
            }

            await Context.Create(entity);

            return entity;
        }

        public async Task Delete(T entity)
        {
            await Context.Delete<T>(entity.Id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return (await Context.GetQueryableAsync<T>()).ToList();
        }

        public async Task<T> Get(Guid id)
        {
            return (await Context.GetQueryableAsync<T>()).SingleOrDefault(u => u.Id == id);
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}
