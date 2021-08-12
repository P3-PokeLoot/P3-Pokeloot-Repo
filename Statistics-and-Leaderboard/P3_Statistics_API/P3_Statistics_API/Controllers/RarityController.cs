using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisinessLayerMethods;
using RepositoryModels;

namespace P3_Statistics_API.Controllers
{

    //[Route("api/[controller]")]
    [ApiController]
    public class RarityController : ControllerBase
    {
        private readonly ILogger<CoinsController> logger;
        private readonly IRarityMethods _RarityMethods;

        public RarityController(IRarityMethods rarityMethods, ILogger<CoinsController> logger)
        {
            this.logger = logger;
            _RarityMethods = rarityMethods;
        }

        [HttpGet("api/[action]")]
        public IActionResult UserListByMostRarityCategory(string rarityCategory, int maxnumber)
        {
            List<UserRarityMapperModel> result;
            result = _RarityMethods.UserListByMostRarityCategory(rarityCategory, maxnumber);
            if (result == null)
                return StatusCode(404);
            else
                return StatusCode(201, result);
            //return result;
        }
        [HttpGet("api/[action]")]
        public IActionResult PercentOfRarityCategory(int userId, string rarityCategory)
        {
            int result;
            result = _RarityMethods.PercentOfRarityCategory(userId, rarityCategory);
            return Ok(result);
        }
        [HttpGet("api/[action]")]
        public IActionResult TotalRarityCategoryForUser(int userId, string rarityCategory)
        {
            int result;
            result = _RarityMethods.TotalRarityCategoryForUser(userId, rarityCategory);
            return Ok(result);
        }


    }
}
