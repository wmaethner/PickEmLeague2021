using System;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    public abstract class BaseRepository : IDisposable
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
    }
}
