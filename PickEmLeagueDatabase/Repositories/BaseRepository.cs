using System;
using Amazon.DynamoDBv2.DataModel;
using PickEmLeagueDatabase.Databases;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    public abstract class BaseRepository : IDisposable //: IBaseRepository
    {
        private bool _disposed;
        //protected RDSDatabaseContext Context { get; }
        protected DynamoDBDatabaseContext Context;

        public BaseRepository(IDatabaseContext dbContext)
        {
            //Context = dbContext as RDSDatabaseContext;
            Context = dbContext as DynamoDBDatabaseContext;
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
