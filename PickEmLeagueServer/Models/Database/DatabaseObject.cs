using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickEmLeagueServer.Models
{
    public abstract class DatabaseObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; private set; }

        public DatabaseObject()
        {
            Guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return Guid.ToString();            
        }
    }
}
