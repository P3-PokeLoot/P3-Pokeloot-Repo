using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisinessLayerMethods;


namespace P3_Statistics_API.Controllers
{
    public class CoinsController : Controller
    {


        private readonly ILogger<CoinsController> logger;
        private readonly ILeaderboardMethods _LeaderboardMethods;

        public CoinsController(ILeaderboardMethods leaderboardMethods, ILogger<CoinsController> logger)
        {
            this.logger = logger;
            _LeaderboardMethods = leaderboardMethods;
        }



        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("api/TopCurrentBalanceList")]
        public IActionResult TopCurrentBallanceController(int maxnumber )
        {
            var result = _LeaderboardMethods.TopCurrentBallance(maxnumber);

            if (result == null)
                return StatusCode(404);
            else
                return StatusCode(201, result);
        }

        [HttpGet("api/TopEarnedCoinsist")]
        public IActionResult TopEarnedCoinsController(int maxnumber)
        {
            var result = _LeaderboardMethods.TopEarnedCoins(maxnumber);

            if (result == null)
                return StatusCode(404);
            else
                return StatusCode(201, result);
        }


        [HttpGet("api/TopSpentCoinsist")]
        public IActionResult TopSpentCoinsController(int maxnumber)
        {
            var result = _LeaderboardMethods.TopSpentCoins(maxnumber);

            if (result == null)
                return StatusCode(404);
            else
                return StatusCode(201, result);
        }

        [HttpGet("api/TopCompletedCollection")]
        public IActionResult TopCompletedCollectionController(int maxnumber)
        {
            var result = _LeaderboardMethods.TopPercentageCompletedCollection(maxnumber);

            if (result == null)
                return StatusCode(404);
            else
                return StatusCode(201, result);
        }

    }
}
