namespace FootballCatalog30.Api.Models
{
    public class Country
    {
        public int Id {  get; init; }
        public string Title { get; init; } = string.Empty;
        public IEnumerable<FootballPlayer> Players { get; init; } = new List<FootballPlayer>();
    }
}
