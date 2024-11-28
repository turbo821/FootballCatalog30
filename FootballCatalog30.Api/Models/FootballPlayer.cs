namespace FootballCatalog30.Api.Models
{
    public class FootballPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public int CommandId { get; set; }
        public FootballCommand? Command { get; set; }

        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
