using AutoMapper;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueIOC.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, PickEmLeagueDatabase.Entities.User>().ReverseMap();
        }
    }
}
