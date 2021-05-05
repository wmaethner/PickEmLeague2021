using System;
using System.Linq;
using System.Threading.Tasks;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IDatabaseContext
    {
        Task Create<T>(T entity) where T : class;
        Task Delete<T>(object key) where T : class;
        IQueryable<T> GetQueryable<T>() where T : class;
        T Get<T>(object key) where T : class;
        Task<int> SaveChangesAsync();
    }
}
