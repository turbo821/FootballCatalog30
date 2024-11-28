using FootballCatalog30.Api.Data;
using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballCatalog30.Api.Repositories
{
    public class FootballRepository : IFootballRepository
    {
        private ApplicationContext _db;
        public FootballRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<FootballPlayer>> GetAllPlayers()
        {
            return await _db.Players.AsNoTracking().ToListAsync();
        }

        public async Task AddPlayer(FootballPlayer player)
        {
            _db.Players.Add(player);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePlayer(FootballPlayer player)
        {
            _db.Players.Update(player);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePlayer(int id)
        {
            var player = await _db.Players.FirstOrDefaultAsync(p => p.Id == id);
            _db.Players.Remove(player!);
            await _db.SaveChangesAsync();
        }
    }
}
