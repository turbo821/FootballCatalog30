using FootballCatalog30.Api.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FootballCatalog30.Api.Interfaces
{
    public interface IFootballRepository
    {
        Task<IEnumerable<FootballPlayer>> GetAllPlayers();
        Task<FootballPlayer?> GetPlayerById(int id);

        Task<FootballPlayer> AddPlayer(FootballPlayer player);
        Task<int> AddAndReturnCommandId(string searchCommandTitle);

        Task<FootballPlayer> UpdatePlayer(FootballPlayer player);

        Task DeletePlayer(int id);

        Task<IEnumerable<FootballCommand>> GetAllCommands();
    }
}
