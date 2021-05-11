using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickEmLeagueServices.Interfaces
{
    public interface IBaseService<TModel, TEntity>
    {
        Task<TModel> Add(TModel model);

        Task<List<TModel>> GetAll();
        Task<TModel> Get(Guid guid);

        Task<TModel> Update(TModel model);
    }
}
