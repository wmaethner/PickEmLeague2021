using System;
using System.Linq;
using System.Threading.Tasks;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IDatabaseContext : IDisposable
    {
        Task Create<T>(T entity) where T : class;
        Task Delete<T>(object key) where T : class;
        Task<IQueryable<T>> GetQueryableAsync<T>() where T : class;
        T Get<T>(object key) where T : class;
        Task<int> SaveChangesAsync();
    }
}
