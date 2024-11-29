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
            return Ok(players);
        }

        [HttpPost]
        public IActionResult AddPlayer(FootballPlayer player)
        {
            _footballService.AddPlayer(player);
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdatePlayer(FootballPlayer player)
        {
            _footballService.UpdatePlayer(player);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            _footballService.DeletePlayer(id);
            return Ok();
        }
    }
}
