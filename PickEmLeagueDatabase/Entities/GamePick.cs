using System;
using Amazon.DynamoDBv2.DataModel;
using PickEmLeague.Global.Shared.Enums;

namespace PickEmLeagueDatabase.Entities
{
    [DynamoDBTable("GamePicks")]
    public class GamePick : GuidBasedEntity
    {
        public Guid Game { get; set; }
        public Guid UserId { get; set; }
        public GameResultEnum Pick { get; set; }
        public int Amount { get; set; }
        public bool UserLocked { get; set; }
    }
}