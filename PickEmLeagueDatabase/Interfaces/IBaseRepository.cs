using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IBaseRepository<T> where T : GuidBasedEntity
    {
        Task<T> Add(T entity);

        Task Delete(T entity);

        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);

        Task SaveChanges();
    }
}
