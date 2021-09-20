using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IGamePickService _gamePickService;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gamerepository, IUserRepository userRepository,
            IGamePickService gamePickService, ITeamRepository teamRepository, IMapper mapper)
        {
            _gameRepository = gamerepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _gamePickService = gamePickService;
            _mapper = mapper;
        }

        public async Task<Game> CreateForWeek(int week)
        {
            return _mapper.Map<Game>(
                await _gameRepository.CreateAsync(
                    new PickEmLeagueDatabase.Entities.Game() { Week = week }));
        }

        public IEnumerable<Game> GetForWeek(int week)
        {
            return _mapper.Map<IEnumerable<Game>>(_gameRepository.GetByWeek(week));
        }

        public async Task<bool> DeleteGame(long id)
        {
            //Need to adjust user pick wagers for the week of this game
            var users = _userRepository.GetAll();
            var game = await _gameRepository.GetById(id);

            // Ensure picks are created then update the wagers above the game deleted (shift down)
            foreach (var user in users)
            {
                var picks = (await _gamePickService.GetByUserAndWeekAsync(user.Id, game.Week)).ToList();
                var wagerToDelete = picks.Find(p => p.GameId == id).Wager;
                var ordered = picks.OrderBy(p => p.Wager);

                for (int index = wagerToDelete; index < ordered.Count(); index++)
                {
                    picks[index].Wager = index;
                }

                await _gamePickService.UpdateByUserAndWeekAsync(picks);
            }

            await _gameRepository.DeleteAsync(id);

            return true;
        }

        public async Task AddScheduleAsync(StreamReader stream)
        {
            var teams = _teamRepository.GetAll();
            // Team to opponents
            var teamsGames = new Dictionary<string, string[]>();
            var gamesByWeek = new Dictionary<int, List<PickEmLeagueDatabase.Entities.Game>>();

            while (!stream.EndOfStream)                          //get all the content in rows 
            {
                string[] row = stream.ReadLine().Split(',');
                teamsGames[TeamMap(row[0])] = row.Skip(1).ToArray();
            }

            foreach (var kvp in teamsGames)
            {
                var team = GetTeam(kvp.Key, teams);

                for (int week = 0; week < kvp.Value.Length; week++)
                {
                    // Create game list if needed
                    if (!gamesByWeek.ContainsKey(week))
                    {
                        gamesByWeek[week] = new List<PickEmLeagueDatabase.Entities.Game>();
                    }

                    // Check if game exists for this team already
                    if ((gamesByWeek[week].Find(g => g.AwayTeam.Name == team.Name) != null) ||
                        (gamesByWeek[week].Find(g => g.HomeTeam.Name == team.Name) != null))
                    {
                        continue;
                    }

                    var opponentString = kvp.Value[week];
                    if (opponentString == "BYE")
                    {
                        continue;
                    }

                    var atOpponent = opponentString[0] == '@';
                    var opponent = GetTeam(TeamMap(atOpponent ? opponentString.Substring(1) : opponentString), teams);

                    var game = new PickEmLeagueDatabase.Entities.Game()
                    {
                        AwayTeam = atOpponent ? team : opponent,
                        HomeTeam = atOpponent ? opponent : team,
                        GameTime = DateTime.Now,
                        Week = week + 1
                    };
                    gamesByWeek[week].Add(game);
                }
            }

            foreach (var kvp in gamesByWeek)
            {
                foreach (var game in kvp.Value)
                {
                    await _gameRepository.CreateAsync(game);
                }
            }

        }

        public bool IsWeekDone(int week)
        {
            foreach (var game in GetForWeek(week))
            {
                if (game.GameResult == PickEmLeagueShared.Enums.GameResult.NotPlayed)
                {
                    return false;
                }
            }

            return true;
        }

        private PickEmLeagueDatabase.Entities.Team GetTeam(string teamName, IEnumerable<PickEmLeagueDatabase.Entities.Team> teams)
        {
            return teams.First(t => t.Name == teamName);
        }

        private string TeamMap(string abbreviation)
        {
            switch (abbreviation)
            {
                case "ARI": return "Cardinals";
                case "ATL": return "Falcons";
                case "BAL": return "Ravens";
                case "BUF": return "Bills";
                case "CAR": return "Panthers";
                case "CHI": return "Bears";
                case "CIN": return "Bengals";
                case "CLE": return "Browns";
                case "DAL": return "Cowboys";
                case "DEN": return "Broncos";
                case "DET": return "Lions";
                case "GB": return "Packers";
                case "HOU": return "Texans";
                case "IND": return "Colts";
                case "JAX": return "Jaguars";
                case "KC": return "Chiefs";
                case "LV": return "Raiders";
                case "LAR": return "Rams";
                case "LAC": return "Chargers";
                case "MIA": return "Dolphins";
                case "MIN": return "Vikings";
                case "NE": return "Patriots";
                case "NO": return "Saints";
                case "NYG": return "Giants";
                case "NYJ": return "Jets";
                case "PHI": return "Eagles";
                case "PIT": return "Steelers";
                case "SF": return "49ers";
                case "SEA": return "Seahawks";
                case "TB": return "Buccaneers";
                case "TEN": return "Titans";
                case "WSH": return "Football Team";
            }
            return "";
        }
    }
}
