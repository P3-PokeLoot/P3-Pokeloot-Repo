using System;
using System.Linq;
using System.Threading.Tasks;
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
        //private readonly DataContext _dataContext;
        private readonly IBusinessModel _businessModel;

        public GamesController(IBusinessModel businessModel, ILogger<GamesController> logger, DataContext dataContext)
        {
            _businessModel = businessModel;
            _logger = logger;
            //_dataContext = dataContext;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetGameInfoListAsync()
        {
            var games = await _businessModel.GameInfoListAsync();
            if (games != null)
                return StatusCode(200, games);
            else
                return StatusCode(500);
        }

        [HttpGet("Wtp")]
        public IActionResult GetRandomPokemon()
        {
            var pokemon = _businessModel.WhosThatPokemonGame();
            return StatusCode(200, pokemon);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddGameInfo(GameInfo gameInfo)
        {
            if (await _businessModel.AddGameInfoAsync(gameInfo))
                return StatusCode(201);
            else
                return StatusCode(500);
        }

        [HttpGet("RpsWin/{userId}")]
        public IActionResult RpsWin(int userId)
        {
            var success = _businessModel.RpsWin(userId);
            return StatusCode(200, success);
        }

        [HttpGet("RpsLose/{userId}")]
        public IActionResult RpsLose(int userId)
        {
            var success = _businessModel.RpsLose(userId);
            return StatusCode(200, success);
        }

        [HttpGet("RpsRecord/{userId}")]
        public IActionResult RpsRecord(int userId)
        {
            var success = _businessModel.RpsRecord(userId);
            return StatusCode(200, success);
        }

        [HttpGet("WtpWin/{userId}")]
        public IActionResult WtpWin(int userId)
        {
            var success = _businessModel.WtpWin(userId);
            return StatusCode(200, success);
        }

        [HttpGet("WtpLose/{userId}")]
        public IActionResult WtpLose(int userId)
        {
            var success = _businessModel.WtpLose(userId);
            return StatusCode(200, success);
        }

        [HttpGet("WtpRecord/{userId}")]
        public IActionResult WtpRecord(int userId)
        {
            var success = _businessModel.WtpRecord(userId);
            return StatusCode(200, success);
        }

        [HttpGet("CapWin/{userId}")]
        public IActionResult CapWin(int userId)
        {
            var success = _businessModel.CapWin(userId);
            return StatusCode(200, success);
        }

        [HttpGet("CapLose/{userId}")]
        public IActionResult CapLose(int userId)
        {
            var success = _businessModel.CapLose(userId);
            return StatusCode(200, success);
        }

        [HttpGet("CapRecord/{userId}")]
        public IActionResult CapRecord(int userId)
        {
            var success = _businessModel.CapRecord(userId);
            return StatusCode(200, success);
        }
    }
}