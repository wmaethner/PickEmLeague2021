using System;
using AutoMapper;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueIOC.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, PickEmLeagueDatabase.Entities.Game>().ReverseMap();
        }
    }
}
