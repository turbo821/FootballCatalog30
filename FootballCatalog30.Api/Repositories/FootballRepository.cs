using FootballCatalog30.Api.Data;
using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            IEnumerable<FootballPlayer> players = await _db.Players.AsNoTracking().Include(p => p.Command).Include(p => p.Country).ToListAsync();
            return players;
        }

        public async Task<FootballPlayer?> GetPlayerById(int id)
        {
            return await _db.Players.Include(p => p.Command).Include(p => p.Country).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<FootballPlayer> AddPlayer(FootballPlayer player)
        {
            _db.Players.Add(player);
            await _db.SaveChangesAsync();

            FootballPlayer? addedPlayer = await GetPlayerById(player.Id);
            return addedPlayer!;
        }

        public async Task<int> AddAndReturnCommandId(string searchCommandTitle)
        {
            FootballCommand? command = await _db.Commands.FirstOrDefaultAsync(c => c.Title == searchCommandTitle);
            if (command is null)
            {
                command = await CreateCommand(searchCommandTitle);
            }
            return command.Id;
        }

        private async Task<FootballCommand> CreateCommand(string commandTitle)
        {
            FootballCommand newCommand = new FootballCommand() { Title = commandTitle };
            _db.Commands.Add(newCommand);
            await _db.SaveChangesAsync();
            return newCommand;
        }

        public async Task<FootballPlayer> UpdatePlayer(FootballPlayer player)
        {
            if (_db.Entry(player).State == EntityState.Detached)
            {
                _db.Players.Attach(player);
            }

            _db.Players.Update(player);
            await _db.SaveChangesAsync();

            FootballPlayer? updatePlayer = await GetPlayerById(player.Id);
            return updatePlayer!;
        }

        public async Task DeletePlayer(int id)
        {
            var player = await _db.Players.FirstOrDefaultAsync(p => p.Id == id);
            _db.Players.Remove(player!);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<FootballCommand>> GetAllCommands()
        {
            return await _db.Commands.AsNoTracking().ToListAsync();
        }
    }
}
