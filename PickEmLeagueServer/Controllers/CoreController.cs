using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueAPI.Controllers
{
    public class CoreController<TModel, TEntity, TService> : ControllerBase
        where TModel : GuidBasedModel
        where TService : IBaseService<TModel, TEntity>        
    {
        TService _service;
        public CoreController(TService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<TModel> Create([FromBody] TModel request)
        {
            //Console.WriteLine($"create new user {request.FirstName}");
            return await _service.Add(request);
        }

        [HttpGet]
        public async Task<IEnumerable<TModel>> GetAsync()
        {
            //Console.WriteLine("user get request");
            //_logger.LogInformation("Get user list");

            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<TModel> GetAsync([FromRoute] Guid id)
        {
            Console.WriteLine($"user get request with id {id}");
            TModel model = await _service.Get(id);
            if (model == null)
            {
                throw new Exception($"Null user for id {id}");
            }
            return model;
        }

        [HttpPut]
        public async Task<TModel> Update([FromBody] TModel user)
        {
            return await _service.Update(user);
        }
    }
}
