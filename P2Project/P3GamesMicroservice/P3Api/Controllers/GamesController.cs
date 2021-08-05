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
    }
}