using System;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Databases
{
    public class DynamoDBDatabaseContext //: IDatabaseContext
    {
        public DynamoDBDatabaseContext()
        {
        }

        public Task Create<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(object key) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(object key) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
