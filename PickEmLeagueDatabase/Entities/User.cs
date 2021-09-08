namespace PickEmLeagueDatabase.Entities
{
    public class User : DbEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
    }
}
