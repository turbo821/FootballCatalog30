using FootballCatalog30.Api.Contracts;
using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Extensions;

namespace FootballCatalog30.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballController : ControllerBase
    {
        private readonly IFootballService _footballService;
        private readonly IHubContext<PlayersHub> _hubContext;

        public FootballController(IFootballService footballService, IHubContext<PlayersHub> hubContext)
        {
            _footballService = footballService;
            _hubContext = hubContext;
        }

        [HttpGet("Players")]
        public async Task<IActionResult> GetAllPlayers()
        {
            IEnumerable<FootballPlayer> players = await _footballService.GetAllPlayers();
            IEnumerable<GetPlayerCatalogDto> playerDtos = players.Select(p => new GetPlayerCatalogDto(
                p.Id,
                p.Name, 
                p.Surname, 
                p.Sex, 
                p.BirthDate, 
                p.Command!.Title, 
                p.Country!.Title)
            );

            return Ok(playerDtos);
        }

        [HttpGet]
        [Route("Players/{id}")]
        public async Task<IActionResult> GetPlayerToEdit([FromRoute] int id)
        {
            var player = await _footballService.GetPlayerById(id);
            if(player == null)
            {
                return BadRequest();
            }

            UpdatePlayerDto playerDto = new(
                player.Id,
                player.Name,
                player.Surname,
                player.Sex,
                player.BirthDate,
                player.Command!.Title,
                player.CountryId
                );

            return Ok(playerDto);
        }

        [HttpPost("Players")]
        public async Task<IActionResult> AddPlayer([FromBody] AddPlayerDto playerDto)
        {
            FootballPlayer player = new FootballPlayer()
            {
                Name = playerDto.Name,
                Surname = playerDto.Surname,
                Sex = playerDto.Sex,
                BirthDate = playerDto.BirthDate,
                CommandId = await _footballService.GetCommandId(playerDto.CommandTitle),
                CountryId = playerDto.CountryId
            };

            var addPlayer = await _footballService.AddPlayer(player);

            var addedPlayer = new GetPlayerCatalogDto(
                addPlayer.Id,
                addPlayer.Name,
                addPlayer.Surname,
                addPlayer.Sex,
                addPlayer.BirthDate,
                addPlayer.Command!.Title,
                addPlayer.Country!.Title
            );

            await _hubContext.Clients.All.SendAsync("PlayerAdded", addedPlayer);

            return Ok();
        }

        [HttpPatch("Players")]
        public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerDto playerDto)
        {
            FootballPlayer player = new FootballPlayer()
            {
                Id = playerDto.Id,
                Name = playerDto.Name,
                Surname = playerDto.Surname,
                Sex = playerDto.Sex,
                BirthDate = playerDto.BirthDate,
                CommandId = await _footballService.GetCommandId(playerDto.CommandTitle),
                CountryId = playerDto.CountryId
            };
            var updPlayer = await _footballService.UpdatePlayer(player);

            var updatePlayer = new GetPlayerCatalogDto(
                updPlayer.Id,
                updPlayer.Name,
                updPlayer.Surname,
                updPlayer.Sex,
                updPlayer.BirthDate,
                updPlayer.Command!.Title,
                updPlayer.Country!.Title
            );

            await _hubContext.Clients.All.SendAsync("PlayerUpdated", updatePlayer);

            return Ok();
        }

        [HttpDelete]
        [Route("Players/{id}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] int id)
        {
            await _footballService.DeletePlayer(id);

            await _hubContext.Clients.All.SendAsync("PlayerDeleted", id);
            return Ok();
        }

        [HttpGet("Commands")]
        public async Task<IActionResult> GetCommands()
        {
            var commands = await _footballService.GetAllCommands();
            IEnumerable<CommandDto> result = commands.Select(c => new CommandDto(c.Id, c.Title));
            return Ok(result);
        }
    }
}
