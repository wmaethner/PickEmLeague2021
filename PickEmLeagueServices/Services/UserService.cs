using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
//using PickEmLeagueDatabase.Entities;


namespace PickEmLeagueServices.Services
{
    public class UserService : IUserService
    {
        DatabaseContext _dbContext;
        IMapper _mapper;

        public UserService(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> AddUser(string email, string firstName, string lastName)
        {
            PickEmLeagueDatabase.Entities.User user = new PickEmLeagueDatabase.Entities.User()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Guid = Guid.NewGuid(),
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<User>(user);
        }

        public async Task<List<User>> GetUsers()
        {
            return _mapper.Map<List<User>>(await _dbContext.Users.ToListAsync());
        }

        public async Task<User> GetUser(Guid guid)
        {
            return _mapper.Map<User>(await _dbContext.Users.SingleOrDefaultAsync(u => u.Guid == guid));
        }

        
    }
}
