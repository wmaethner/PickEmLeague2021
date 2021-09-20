using System;
namespace PickEmLeagueModels.Models.Responses
{
    public class WeekWinnerResponse
    {
        public User? Winner { get; set; }
        public bool FoundWinner { get; set; }
    }
}
