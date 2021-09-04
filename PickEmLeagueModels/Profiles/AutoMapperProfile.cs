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
            CreateMap<PickEmLeagueDatabase.Entities.Game, Game>()
                .ReverseMap()
                .ForMember(m => m.HomeTeam, opts => opts.Ignore())
                .ForMember(m => m.AwayTeam, opts => opts.Ignore());
            CreateMap<PickEmLeagueDatabase.Entities.Team, Team>().ReverseMap();
        }
    }
}
