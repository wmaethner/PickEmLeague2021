using System;
using AutoMapper;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Implementations
{
    public class MiscAdminRepository : CrudRepository<MiscAdmin, PickEmLeagueModels.Models.MiscAdmin>, IMiscAdminRepository
    {
        public MiscAdminRepository(PickEmLeagueDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
