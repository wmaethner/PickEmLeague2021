using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Mocks
{
    public class MockCrudRepository<TEntity, TModel> : ICrudRepository<TEntity, TModel>
        where TEntity : DbEntity, new()
        where TModel : DbModel
    {
        private long _currentId = 1;
        private Dictionary<long, TEntity> _entities = new Dictionary<long, TEntity>();

        private readonly IMapper _mapper;

        public MockCrudRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<TEntity> CreateAsync()
        {
            return await CreateAsync(new TEntity());
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.Id = _currentId++;
            _entities.Add(entity.Id, entity);
            return Task.FromResult(entity);
        }

        public Task<bool> DeleteAsync(long id)
        {
            return Task.FromResult(_entities.Remove(id));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.Values;
        }

        public Task<TEntity> GetById(long id)
        {
            return Task.FromResult(_entities.ContainsKey(id) ? _entities[id] : null);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _entities.Values.AsQueryable();
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<TEntity> UpdateAsync(TModel model)
        {
            var entity = await GetById(model.Id);
            _mapper.Map(model, entity);
            _entities[entity.Id] = entity;
            return entity;
        }

        public void ClearDb()
        {
            _entities.Clear();
        }
    }
}
