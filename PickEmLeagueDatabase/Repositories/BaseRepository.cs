using System;
using Amazon.DynamoDBv2.DataModel;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    public abstract class BaseRepository : IDisposable //: IBaseRepository
    {
        private bool _disposed;
        protected RDSDatabaseContext Context { get; }
        protected DynamoDBContext DynamoContext;

        public BaseRepository(IDatabaseContext dbContext)
        {
            Context = dbContext as RDSDatabaseContext;
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
