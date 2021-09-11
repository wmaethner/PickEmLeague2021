using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudController<TEntity, TModel> : ControllerBase
        where TEntity : DbEntity
        where TModel : DbModel
    {
        private readonly ICrudRepository<TEntity, TModel> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CrudController(ICrudRepository<TEntity, TModel> repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<TModel> Add()
        {
            _logger.LogInformation($"Adding new {typeof(TModel)}");
            return MapEntity(await _repository.CreateAsync());
        }

        [HttpGet("get-all")]
        public IEnumerable<TModel> GetAll()
        {
            _logger.LogInformation($"Getting all {typeof(TModel)}");
            IEnumerable<TEntity> entities = _repository.GetAll();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        [HttpGet("get")]
        public async Task<TModel> Get(long id)
        {
            _logger.LogInformation($"Getting {typeof(TModel)} with id {id}");
            return MapEntity(await _repository.GetById(id));
        }

        [HttpPut("update")]
        public async Task Edit(TModel model)
        {
            _logger.LogInformation($"Editing {typeof(TModel)} with id {model.Id}");
            await _repository.UpdateAsync(model);
        }

        [HttpDelete("delete")]
        public async Task Delete(long id)
        {
            _logger.LogInformation($"Deleting {typeof(TModel)} with id {id}");
            await _repository.DeleteAsync(id);
        }

        private TModel MapEntity(TEntity entity)
        {
            return _mapper.Map<TModel>(entity);
        }
    }
}
