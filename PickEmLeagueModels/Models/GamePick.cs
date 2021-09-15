using System;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueModels.Models
{
    public class GamePick : DbModel
    {
        public GameResult Pick { get; set; }
        public int Wager { get; set; }
        public bool Locked { get; set; }
        public bool Editable => DateTime.UtcNow < Game.GameTime;
        public bool CorrectPick
        {
            get
            {
                return Pick == GameResult.NotPlayed ? false :
                    Pick == Game.GameResult ? true : false;
            }
        }


        public User User { get; set; }
        public Game Game { get; set; }

        public long UserId { get; set; }
        public long GameId { get; set; }
    }
}
