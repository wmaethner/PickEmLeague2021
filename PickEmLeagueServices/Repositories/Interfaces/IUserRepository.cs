﻿using System;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface IUserRepository : ICrudRepository<User, PickEmLeagueModels.Models.User>
    {
    }
}
