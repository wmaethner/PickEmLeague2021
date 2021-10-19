using System;
using AutoMapper;
using PickEmLeagueModels.Models;

namespace PickEmLeagueModels.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PickEmLeagueDatabase.Entities.User, User>()
                .ReverseMap();

            CreateMap<PickEmLeagueDatabase.Entities.Team, Team>().ReverseMap();

            CreateMap<PickEmLeagueDatabase.Entities.Game, Game>()
                .ForMember(model => model.GameTime, opts => opts.MapFrom(e => DateTime.SpecifyKind(e.GameTime, DateTimeKind.Utc)))
                .ForMember(model => model.GameTimeString, opts => opts.MapFrom(e => e.GameTime.ToLocalTime().ToString("MM/dd/yyyy hh:mm")))
                .ReverseMap()
                .ForMember(model => model.HomeTeam, opts => opts.Ignore())
                .ForMember(model => model.AwayTeam, opts => opts.Ignore());

            CreateMap<PickEmLeagueDatabase.Entities.GamePick, GamePick>()
                .ReverseMap()
                .ForMember(model => model.Game, opts => opts.Ignore())
                .ForMember(model => model.User, opts => opts.Ignore());

            CreateMap<PickEmLeagueDatabase.Entities.MiscAdmin, MiscAdmin>().ReverseMap();
        }
    }
}
