using System;
using System.Linq;
using AutoMapper;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Mocks
{
    public class MockUserRepository : MockCrudRepository<User, PickEmLeagueModels.Models.User>, IUserRepository
    {
        public MockUserRepository(IMapper mapper) : base(mapper)
        {
        }

        public User GetByEmail(string email)
        {
            return GetQueryable().FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
