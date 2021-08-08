using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public GamesController(IBusinessModel businessModel, ILogger<GamesController> logger, DataContext dataContext, IWebHostEnvironment hostEnvironment)
        {
            _businessModel = businessModel;
            _logger = logger;
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Returns a list of all  games description
        /// </summary>
        /// <returns></returns>

        [HttpGet("List")]
        public IActionResult GetGameInfoList()
        {
            List<GameDetail> gameDetails;

            gameDetails = _businessModel.GetGameInfoList();

            return StatusCode(200, gameDetails);
        }

        [HttpGet("Wtp")]
        public IActionResult GetRandomPokemon()
        {
            var pokemon = _businessModel.WhosThatPokemonGame();
            return StatusCode(200, pokemon);
        }

        [HttpGet("SingleGame/{id}")]
        public IActionResult GetSingleGame(int id)
        {
            GameDetail gameInfo = _businessModel.SingleGame(id);

            if (gameInfo != null)
            {
                return StatusCode(200, gameInfo);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a game with only descriptions
        /// </summary>
        /// <param name="gameDetail"></param>
        /// <returns></returns>
        [HttpPost("CreateGame")]
        public async Task<ActionResult<GameInfo>> CreateGame([FromForm] GameDetail gameDetail)
        {
            gameDetail.ImageName = await SaveImage(gameDetail.ImageFile);

            GameInfo gameInfo = _businessModel.CreateGame(gameDetail);


            if (gameInfo != null)
            {
                return StatusCode(201, gameInfo);
            }
            else
            {
                return StatusCode(422, gameInfo);
            }
        }

        /// <summary>
        /// Modifies an existing game 
        /// </summary>
        /// <param name="gameDetail"></param>
        /// <returns></returns>

        [HttpPatch("ModifyGame")]
        public async Task<ActionResult<GameInfo>> ModifyGame([FromForm] GameDetail gameDetail)
        {
            if (gameDetail.ImageFile != null)
            {
                gameDetail.ImageName = await SaveImage(gameDetail.ImageFile);

            }

            GameInfo gameInfo = _businessModel.ModifyGame(gameDetail);


            if (gameInfo != null)
            {
                return StatusCode(200, gameInfo);
            }
            else
            {
                return StatusCode(422, gameInfo);
            }
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteGame(int id)
        {
            GameInfo gameInfo = _businessModel.DeleteGame(id);


            if (gameInfo.ImagePath != null || gameInfo.ImagePath != "")
            {
                DeleteImage(gameInfo.ImagePath);
            }

            if (gameInfo != null)
            {
                return StatusCode(200, gameInfo);
            }
            else
            {
                return NotFound();
            }

        }
        // Stores the image statically in the Image folder and returns a modified version of the name. 
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("ttmmssffff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath)) 
            {
                System.IO.File.Delete(imagePath);
            }
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