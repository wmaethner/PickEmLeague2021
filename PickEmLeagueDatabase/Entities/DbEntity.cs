using System.ComponentModel.DataAnnotations.Schema;

namespace PickEmLeagueDatabase.Entities
{
    public class DbEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
