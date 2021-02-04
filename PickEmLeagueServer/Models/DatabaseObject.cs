using System;
namespace PickEmLeagueServer.Models
{
    public abstract class DatabaseObject : ICloneable
    {
        public string Id { get; private set; }

        public DatabaseObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public abstract object Clone();
    }
}
