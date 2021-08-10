using BuisinessLayerMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3_Statistics_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly ILogger<CoinsController> logger;
        private readonly IAchievementMethods _AchievementMethods;

        public AchievementController(IAchievementMethods achievementMethods, ILogger<CoinsController> logger)
        {
            this.logger = logger;
            _AchievementMethods = achievementMethods;
        }

        [HttpGet("[action]")]
        public IActionResult UserListByMostAchievements(int maxnumber)
        {
            List<UserAchievementMapperModel> result;
            result = _AchievementMethods.UserListByMostAchievements(maxnumber);
            if (result == null)
                return StatusCode(404);
            else
                return StatusCode(201, result);
        }
        [HttpGet("[action]")]
        public IActionResult PercentOfRarityCategory(string achievementName)
        {
            int result;
            result = _AchievementMethods.PercentOfAchievementType(achievementName);
            return Ok(result);
        }
    }
}
