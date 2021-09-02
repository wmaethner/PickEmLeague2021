using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Implementations
{
    public class CrudRepository<TEntity, TModel> : ICrudRepository<TEntity, TModel>
        where TEntity : DbEntity, new()
        where TModel : DbModel
    {
        private readonly PickEmLeagueDbContext _dbContext;
        private readonly IMapper _mapper;

        public CrudRepository(PickEmLeagueDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TEntity> CreateAsync()
        {
            var entity = await _dbContext.AddAsync(new TEntity());
            await Save();
            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = _dbContext.Remove(GetAsync(id));
            await Save();
            return response.State == EntityState.Deleted;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetQueryable().ToList();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await GetQueryable().FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> UpdateAsync(TModel model)
        {
            var entity = await GetAsync(model.Id);
            _mapper.Map(model, entity);
            await Save();
            return entity;
        }

        private async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
