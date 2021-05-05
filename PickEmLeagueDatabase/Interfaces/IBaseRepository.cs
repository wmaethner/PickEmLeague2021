using System;
using System.Linq;
using System.Threading.Tasks;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IBaseRepository : IDisposable
    {
        void Delete<T>(object key) where T : class;
        IQueryable<T> GetQueryable<T>() where T : class;
        void Insert<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();
    }
}
