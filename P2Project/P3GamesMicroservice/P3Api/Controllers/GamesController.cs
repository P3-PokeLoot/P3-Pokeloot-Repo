using System;
using System.Linq;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using P3Database;

namespace P3Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly DataContext _dataContext;
        private readonly IBusinessModel _businessModel;

        public GamesController(IBusinessModel businessModel, ILogger<GamesController> logger, DataContext dataContext)
        {
            _businessModel = businessModel;
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet("List")]
        public IActionResult GetGameInfoList()
        {
            var games = _dataContext.GameInfos.ToList();
            return StatusCode(200, games);
        }

        [HttpGet("Wtp")]
        public IActionResult GetRandomPokemon()
        {
            var pokemon = _businessModel.WhosThatPokemonGame();
            return StatusCode(200, pokemon);
        }

        [HttpPost("Add")]
        public IActionResult AddGameInfo(GameInfo gameInfo)
        {
            if(gameInfo != null && gameInfo.Title.Length > 0)
            {
                _dataContext.GameInfos.Add(gameInfo);
                _dataContext.SaveChanges();
                return StatusCode(201);
            }

            return StatusCode(500);
        }

        [HttpPost("RpsWin")]
        public IActionResult RpsWin(int userId)
        {
            var success = _businessModel.RpsWin(userId);
            if(success)
            {
                return StatusCode(200, success);
            }
            return BadRequest(new { message = "Error, Something went wrong.", status = -1 });
        }

        [HttpPost("RpsLose")]
        public IActionResult RpsLose(int userId)
        {
            var success = _businessModel.RpsLose(userId);
            if (success)
            {
                return StatusCode(200, success);
            }
            return BadRequest(new { message = "Error, Something went wrong.", status = -1 });
        }

        [HttpGet("RpsRecord/{userId}")]
        public IActionResult RpsRecord(int userId)
        {
            var success = _businessModel.RpsRecord(userId);
            return StatusCode(200, success);
        }

        [HttpPost("WtpWin")]
        public ActionResult WtpWin(int userId)
        {
            var success = _businessModel.WtpWin(userId);
            if (success)
            {
                return StatusCode(200, success);
            }
            return BadRequest(new { message = "Error, Something went wrong.", status = -1 });
        }

        [HttpPost("WtpLose")]
        public IActionResult WtpLose(int userId)
        {
            var success = _businessModel.WtpLose(userId);
            if (success)
            {
                return StatusCode(200, success);
            }
            return BadRequest(new { message = "Error, Something went wrong.", status = -1 });
        }

        [HttpGet("WtpRecord/{userId}")]
        public IActionResult WtpRecord(int userId)
        {
            var success = _businessModel.WtpRecord(userId);
            return StatusCode(200, success);
        }

        [HttpPost("CapWin")]
        public IActionResult CapWin(int userId)
        {
            var success = _businessModel.CapWin(userId);
            if (success)
            {
                return StatusCode(200, success);
            }
            return BadRequest(new { message = "Error, Something went wrong.", status = -1 });
        }

        [HttpPost("CapLose")]
        public IActionResult CapLose(int userId)
        {
            var success = _businessModel.CapLose(userId);
            if (success)
            {
                return StatusCode(200, success);
            }
            return BadRequest(new { message = "Error, Something went wrong.", status = -1 });
        }

        [HttpGet("CapRecord/{userId}")]
        public IActionResult CapRecord(int userId)
        {
            var success = _businessModel.CapRecord(userId);
            return StatusCode(200, success);
        }
    }
}