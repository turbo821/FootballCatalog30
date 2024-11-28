using FootballCatalog30.Api.Models;
using Microsoft.EntityFrameworkCore;

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
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<FootballPlayer>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.CountryId);

            modelBuilder.Entity<Country>()
                .HasData(
                new Country() { Id = 1, Title = "Россия"},
                new Country() { Id = 2, Title = "США" },
                new Country() { Id = 3, Title = "Италия" }
                );
        }
    }
}
