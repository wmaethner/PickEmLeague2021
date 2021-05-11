using System;
namespace PickEmLeagueDatabase.Entities
{
    public class GuidBasedEntity
    {
        public Guid Id { get; set; }

        public GuidBasedEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
