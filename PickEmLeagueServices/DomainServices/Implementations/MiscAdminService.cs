using System;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class MiscAdminService : IMiscAdminService
    {
        private readonly IMiscAdminRepository _miscAdminRepository;
        private readonly IMapper _mapper;

        public MiscAdminService(IMiscAdminRepository miscAdminRepository, IMapper mapper)
        {
            _miscAdminRepository = miscAdminRepository;
            _mapper = mapper;
        }

        public async Task<MiscAdmin> GetMiscAdminAsync()
        {
            var misc = await _miscAdminRepository.GetById(1);
            if (misc == null)
            {
                misc = await _miscAdminRepository.CreateAsync();
            }
            return _mapper.Map<MiscAdmin>(misc);
        }

        public async Task<bool> UpdateMiscAdminAsync(MiscAdmin miscAdmin)
        {
            await _miscAdminRepository.UpdateAsync(miscAdmin);
            return true;
        }
    }
}
