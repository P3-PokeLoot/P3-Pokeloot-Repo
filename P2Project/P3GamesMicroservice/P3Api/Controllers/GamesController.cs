using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace P3Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;

        public GamesController(ILogger<GamesController> logger)
        {
            logger = _logger;
        }

        [HttpGet("Test")]
        public IActionResult Testing(int i)
        {
            return StatusCode(200, i);
        }

        [HttpGet("{id}")]
        public IActionResult GetGameDetails(int id)
        {
            //Wait until table is defined

            //_context.Games.Where(x => x.id == id).FirstOrDefault();

            return StatusCode(200);
        }
    }
}