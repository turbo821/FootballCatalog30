using FootballCatalog30.Api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FootballCatalog30.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FootballPlayer> Players { get; set; } = null!;
        public DbSet<FootballCommand> Commands { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FootballPlayer>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<FootballCommand>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Country>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<FootballPlayer>()
                .HasOne(p => p.Command)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.CommandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FootballPlayer>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.CountryId);

            SetExampleData(modelBuilder);
        }


        private void SetExampleData(ModelBuilder modelBuilder)
        {
            //var russia = new Country() { Id = 1, Title = "Россия" };
            //var usa = new Country() { Id = 2, Title = "США" };
            //var italia = new Country() { Id = 3, Title = "Италия" };

            //modelBuilder.Entity<Country>()
            //    .HasData(
            //    russia,
            //    usa,
            //    italia
            //    );

            //var zenith = new FootballCommand() { Id = 1, Title = "Зенит" };
            //var locomotive = new FootballCommand() { Id = 2, Title = "Локомотив" };
            //var dallas = new FootballCommand() { Id = 3, Title = "Даллас" };
            //var milan = new FootballCommand() { Id = 4, Title = "Милан" };

            //modelBuilder.Entity<FootballCommand>()
            //    .HasData(
            //    zenith,
            //    locomotive,
            //    dallas,
            //    milan
            //    );

            //var player1 = new FootballPlayer() { Id = 1, Name = "Александр", Surname = "Соболев", BirthDate = new DateTime(1997, 3, 7), CommandId = 1, CountryId = 1 };
            //var player2 = new FootballPlayer() { Id = 2, Name = "Михаил", Surname = "Кержаков", BirthDate = new DateTime(1987, 1, 28), CommandId = 1, CountryId = 1 };
            //var player3 = new FootballPlayer() { Id = 3, Name = "Илья", Surname = "Самошников", BirthDate = new DateTime(1997, 11, 14), CommandId = 2, CountryId = 1 };
            //var player4 = new FootballPlayer() { Id = 4, Name = "Хесус", Surname = "Феррейра", BirthDate = new DateTime(2000, 12, 24), CommandId = 3, CountryId = 2 };
            //var player5 = new FootballPlayer() { Id = 5, Name = "Альваро", Surname = "Мората", BirthDate = new DateTime(1992, 10, 23), CommandId = 4, CountryId = 3 };

            //modelBuilder.Entity<FootballPlayer>()
            //    .HasData(
            //    player1,
            //    player2,
            //    player3,
            //    player4,
            //    player5
            //    );
        }
    }
}
