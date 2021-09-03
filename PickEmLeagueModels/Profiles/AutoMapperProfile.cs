using System;
using AutoMapper;
using PickEmLeagueModels.Models;

namespace PickEmLeagueModels.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PickEmLeagueDatabase.Entities.User, User>().ReverseMap();
            CreateMap<PickEmLeagueDatabase.Entities.Game, Game>().ReverseMap();
        }
    }
}
