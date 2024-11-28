namespace FootballCatalog30.Api.Models
{
    public class FootballCommand
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IEnumerable<FootballPlayer> Players { get; init; } = new List<FootballPlayer>();
    }
}
