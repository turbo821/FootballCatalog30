using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Models;

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

        public async Task AddPlayer(FootballPlayer player)
        {
            await _repo.AddPlayer(player);
        }

        public async Task<int> GetCommandId(string searchCommandTitle)
        {
            return await _repo.AddAndReturnCommandId(searchCommandTitle);
        }

        public async Task UpdatePlayer(FootballPlayer player)
        {
            await _repo.UpdatePlayer(player);
        }

        public async Task DeletePlayer(int id)
        {
            await _repo.DeletePlayer(id);
        }
    }
}
