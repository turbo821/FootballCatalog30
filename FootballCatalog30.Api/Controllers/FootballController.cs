using FootballCatalog30.Api.Contracts;
using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace FootballCatalog30.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballController : ControllerBase
    {
        private readonly IFootballService _footballService;

        public FootballController(IFootballService footballService)
        {
            _footballService = footballService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            IEnumerable<FootballPlayer> players = await _footballService.GetAllPlayers();
            IEnumerable<GetPlayerCatalogDto> playerDtos = players.Select(p => new GetPlayerCatalogDto(
                p.Id,
                p.Name, 
                p.Surname, 
                p.Sex.GetDisplayName(), 
                p.BirthDate, 
                p.Command?.Title, 
                p.Country?.Title)
            );

            return Ok(playerDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlayerToEdit(int id)
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
                player.Country!.Title
                );

            return Ok(playerDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(AddPlayerDto playerDto)
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

            await _footballService.AddPlayer(player);
            return Ok();
        }



        [HttpPatch]
        public async Task<IActionResult> UpdatePlayer(FootballPlayer player)
        {
            await _footballService.UpdatePlayer(player);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            await _footballService.DeletePlayer(id);
            return Ok();
        }
    }
}
