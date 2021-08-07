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
        public async Task<IActionResult> GetRandomPokemon()
        {
            var pokemon = await _businessModel.WhosThatPokemonGameAsync();
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
        public async Task<IActionResult> RpsWin(int userId)
        {
            var success = await _businessModel.RpsWinAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("RpsLose/{userId}")]
        public async Task<IActionResult> RpsLose(int userId)
        {
            var success = await _businessModel.RpsLoseAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("RpsRecord/{userId}")]
        public async Task<IActionResult> RpsRecord(int userId)
        {
            var success = await _businessModel.RpsRecordAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("WtpWin/{userId}")]
        public async Task<IActionResult> WtpWin(int userId)
        {
            var success = await _businessModel.WtpWinAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("WtpLose/{userId}")]
        public async Task<IActionResult> WtpLose(int userId)
        {
            var success = await _businessModel.WtpLoseAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("WtpRecord/{userId}")]
        public async Task<IActionResult> WtpRecord(int userId)
        {
            var success = await _businessModel.WtpRecordAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("CapWin/{userId}")]
        public async Task<IActionResult> CapWin(int userId)
        {
            var success = await _businessModel.CapWinAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("CapLose/{userId}")]
        public async Task<IActionResult> CapLose(int userId)
        {
            var success = await _businessModel.CapLoseAsync(userId);
            return StatusCode(200, success);
        }

        [HttpGet("CapRecord/{userId}")]
        public async Task<IActionResult> CapRecord(int userId)
        {
            var success = await _businessModel.CapRecordAsync(userId);
            return StatusCode(200, success);
        }
    }
}