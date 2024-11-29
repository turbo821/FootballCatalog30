using FootballCatalog30.Api.Models;

namespace FootballCatalog30.Api.Interfaces
{
    public interface IFootballService
    {
        Task<IEnumerable<FootballPlayer>> GetAllPlayers();

        Task AddPlayer(FootballPlayer player);

        Task UpdatePlayer(FootballPlayer player);

        Task DeletePlayer(int id);
    }
}
