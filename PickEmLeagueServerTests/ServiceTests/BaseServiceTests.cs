using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using Xunit;

namespace PickEmLeagueServerTests.ServiceTests
{
    public abstract class BaseServiceTests<TModel, TEntity> where TModel : GuidBasedModel
    {
        protected abstract IBaseService<TModel, TEntity> BaseService();
        public abstract TModel NewModel();
        public abstract TModel UpdateModel(TModel model);

        [Fact]
        public async Task GetWithIdSucceeds()
        {
            TModel model = await AddModel(NewModel());

            Assert.Equal(model, await GetModel(model.Id));
        }

        [Fact]
        public async Task GetAllSucceeds()
        {
            await AddModel(NewModel());
            await AddModel(NewModel());
            await AddModel(NewModel());

            Assert.Equal(3, (await BaseService().GetAll()).Count);
        }

        [Fact]
        public async Task UpdateSucceeds()
        {
            TModel model = NewModel();
            model = await BaseService().Add(model);
            model = UpdateModel(model);

            await BaseService().Update(model);

            Assert.Equal(model, await BaseService().Get(model.Id));
        }

        protected async Task<TModel> AddModel(TModel model)
        {
            return await BaseService().Add(model);
        }

        protected async Task<TModel> GetModel(Guid id)
        {
            return await BaseService().Get(id);
        }

        protected async Task<List<TModel>> GetAllModels()
        {
            return await BaseService().GetAll();
        }
    }
}
