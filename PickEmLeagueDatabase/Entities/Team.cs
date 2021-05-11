using System;
namespace PickEmLeagueDatabase.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        //TODO: Add logo
    }
}
