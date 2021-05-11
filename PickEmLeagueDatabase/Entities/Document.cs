using System;
namespace PickEmLeagueDatabase.Entities
{
    public class Document : GuidBasedEntity
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
