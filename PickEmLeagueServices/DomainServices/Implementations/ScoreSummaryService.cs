using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class ScoreSummaryService : IScoreSummaryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IAwsS3Service _awsS3Service;
        private readonly IMapper _mapper;

        public ScoreSummaryService(IUserRepository userRepository, IGamePickRepository gamePickRepository,
            IAwsS3Service awsS3Service, IMapper mapper)
        {
            _userRepository = userRepository;
            _gamePickRepository = gamePickRepository;
            _awsS3Service = awsS3Service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserSummary>> GetSummariesAsync(int week)
        {
            var summaries = new List<UserSummary>();
            var users = _userRepository.GetAll().ToList();

            //foreach (var user in await MapUsersAsync(users)) 
            foreach (var user in _mapper.Map<IEnumerable<User>>(users))
            {
                summaries.Add(GetUserSummary(user, week));
            }

            return summaries.OrderByDescending(x => x.WeekSummary.WeekScore).ThenBy(y => y.User.Name);
        }

        //private async Task<IEnumerable<User>> MapUsersAsync(IEnumerable<PickEmLeagueDatabase.Entities.User> entities)
        //{
        //    var models = new List<User>();

        //    foreach (var entity in entities)
        //    {
        //        var model = _mapper.Map<User>(entity);
        //        if (string.IsNullOrEmpty(entity.ProfilePictureKey))
        //        {
        //            //model.ProfilePic = await _awsS3Service.GetDefaultUserImageAsync();
        //            model.PicType = "svg";
        //        }
        //        else
        //        {
        //            //model.ProfilePic = await _awsS3Service.GetUserImageAsync(entity.Id);
        //            model.PicType = entity.ProfilePictureKey.Split(".").Last();
        //        }
        //        model.ProfilePic = await _awsS3Service.GetUserImageAsync(entity.Id);

        //        models.Add(model);
        //    }

        //    return models;
        //}

        private UserSummary GetUserSummary(User user, int week)
        {
            var summary = new UserSummary() { User = user };
            var picks = _gamePickRepository.GetByUser(user.Id);

            bool madePick = false;
            bool missedPick = false;

            foreach (var pick in picks)
            {
                if (CorrectPick(pick.Pick, pick.Game))
                {
                    summary.SeasonSummary.SeasonScore += pick.Wager;
                    summary.SeasonSummary.CorrectPicks++;
                    if (pick.Game.Week == week)
                    {
                        summary.WeekSummary.WeekScore += pick.Wager;
                        summary.WeekSummary.CorrectPicks++;
                    }
                }

                if (pick.Game.Week == week)
                {
                    if (pick.Pick == GameResult.NotPlayed)
                    {
                        missedPick = true;
                    }
                    else
                    {
                        madePick = true;
                    }
                }
            }

            summary.WeekSummary.WeekPickStatus =
                (madePick) ? (missedPick ? WeekPickStatus.PartiallyPicked :
                                           WeekPickStatus.FullyPicked)
                           : WeekPickStatus.NotPicked;

            return summary;
        }


        private bool CorrectPick(GameResult usersPick, PickEmLeagueDatabase.Entities.Game game)
        {
            if (usersPick != GameResult.NotPlayed)
            {
                return game.GameResult == usersPick;
            }
            return false;
        }


    }
}
