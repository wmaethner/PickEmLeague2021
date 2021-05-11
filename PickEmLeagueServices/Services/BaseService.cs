using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    public class BaseService<TModel, TEntity> : IBaseService<TModel, TEntity>
        where TEntity : GuidBasedEntity, new()
        where TModel : GuidBasedModel
    {
        IBaseRepository<TEntity> _repository;
        IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _repository = baseRepository;
            _mapper = mapper;
        }

        public async Task<TModel> Add(TModel model)
        {
            TEntity entity = new TEntity();

            var customMapConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<TModel, TEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            });
            var customMapper = customMapConfig.CreateMapper();            
            entity = customMapper.Map<TModel, TEntity>(model);

            await _repository.Add(entity);

            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> Get(Guid guid)
        {
            return _mapper.Map<TModel>(await _repository.Get(guid));
        }

        public async Task<List<TModel>> GetAll()
        {
            return _mapper.Map<List<TModel>>((await _repository.GetAll()).ToList());
        }

        public async Task<TModel> Update(TModel model)
        {
            // RDS Implementation
            //var entity = _userRepository.GetUserAsync(user.Guid);
            //_mapper.Map(user, entity);
            //await _userRepository.SaveChangesAsync();

            // DynamoDB Implementation
            var entity = await _repository.Get(model.Id);
            await _repository.Delete(entity);
            _mapper.Map(model, entity);
            await _repository.Add(entity);

            return model;
        }
    }
}
