﻿using System;
using Amazon.DynamoDBv2.DataModel;

namespace PickEmLeagueDatabase.Entities
{
    [DynamoDBTable("Games")]
    public class Game : GuidBasedEntity
    {
        //public Guid Id { get; set; }
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? Winner { get; set; }
        public DateTime GameTime { get; set; }
    }
}