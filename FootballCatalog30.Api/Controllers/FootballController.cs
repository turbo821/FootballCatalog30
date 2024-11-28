using FootballCatalog30.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace FootballCatalog30.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballController : ControllerBase
    {
        private readonly ILogger<FootballController> _logger;

        public FootballController(ILogger<FootballController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult AddPlayer(FootballPlayer player)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdatePlayer(FootballPlayer player)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            return Ok();
        }
    }
}
