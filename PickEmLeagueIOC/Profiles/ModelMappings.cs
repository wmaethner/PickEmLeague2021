using System;
using AutoMapper;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueIOC.Profiles
{
    public class ModelMappings : Profile
    {
        public ModelMappings()
        {
            CreateMap<User, PickEmLeagueDatabase.Entities.User>().ReverseMap();
            CreateMap<Game, PickEmLeagueDatabase.Entities.Game>().ReverseMap();
        }
    }
}
