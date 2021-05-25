using System;
using PickEmLeague.Global.Shared.Enums;

namespace PickEmLeagueDomain.Models
{
    public class GamePick : GuidBasedModel
    {
        public Guid Game { get; set; }
        public Guid UserId { get; set; }
        public GameResultEnum Pick { get; set; }
        public int Amount { get; set; }
        public bool UserLocked { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as GamePick);
        }

        public bool Equals(GamePick other)
        {
            return other != null &&
                   Game == other.Game &&
                   UserId == other.UserId &&
                   Pick == other.Pick &&
                   Amount == other.Amount &&
                   UserLocked == other.UserLocked;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Game, UserId, Pick, Amount, UserLocked);
        }
    }
}
