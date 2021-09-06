using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface ICrudRepository<TEntity, TModel>
        where TEntity : DbEntity
        where TModel : DbModel
    {
        Task<TEntity> CreateAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetById(long id);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> UpdateAsync(TModel model);
        Task<bool> DeleteAsync(long id);

        IQueryable<TEntity> GetQueryable();
    }
}
