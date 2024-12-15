using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FootballCatalog30.Api.Services
{
    public class FootballService : IFootballService
    {
        private IFootballRepository _repo;

        public FootballService(IFootballRepository repository)
        {
            _repo = repository;
        }

        public async Task<IEnumerable<FootballPlayer>> GetAllPlayers()
        {
            return await _repo.GetAllPlayers();
        }

        public async Task<FootballPlayer?> GetPlayerById(int id)
        {
            return await _repo.GetPlayerById(id);
        }

        public async Task<FootballPlayer> AddPlayer(FootballPlayer player)
        {
            return await _repo.AddPlayer(player);
        }

        public async Task<int> GetCommandId(string searchCommandTitle)
        {
            return await _repo.AddAndReturnCommandId(searchCommandTitle);
        }

        public async Task<FootballPlayer> UpdatePlayer(FootballPlayer player)
        {
            return await _repo.UpdatePlayer(player);
        }

        public async Task DeletePlayer(int id)
        {
            await _repo.DeletePlayer(id);
        }

        public async Task<IEnumerable<FootballCommand>> GetAllCommands()
        {
            return await _repo.GetAllCommands();
        }
    }
}
