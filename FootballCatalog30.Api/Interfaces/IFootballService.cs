using FootballCatalog30.Api.Models;

namespace FootballCatalog30.Api.Interfaces
{
    public interface IFootballService
    {
        Task<IEnumerable<FootballPlayer>> GetAllPlayers();
        Task<FootballPlayer?> GetPlayerById(int id);

        Task AddPlayer(FootballPlayer player);
        Task<int> GetCommandId(string searchCommandTitle);

        Task UpdatePlayer(FootballPlayer player);

        Task DeletePlayer(int id);    
    }
}
