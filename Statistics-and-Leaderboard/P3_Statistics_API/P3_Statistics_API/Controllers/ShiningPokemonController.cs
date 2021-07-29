using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepositoryModels;
using BuisinessLayerMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P3_Statistics_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiningPokemonController : ControllerBase
    {

        private readonly ILeaderboardMethods _leaderboard;

        public ShiningPokemonController(ILeaderboardMethods leaderboard)
        {
            _leaderboard = leaderboard;
        }

        /// <summary>
        /// Display the users pokemon that have more reapeted shiny
        /// </summary>
        /// <param name="userMost"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userMost}")]
        public IActionResult DisplayUserMostShinyPokemon(int userMost)
        {
           
            List<CardCollection> userShinyList = _leaderboard.UserMostShinyPokemon(userMost);

            if (userShinyList == null)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, userShinyList);
            }
        }

        /// <summary>
        /// Display the players with most shinys
        /// </summary>
        /// <param name="topUsers"></param>
        /// <returns></returns>
        [HttpGet("[action]/{topUsers}")]
        public IActionResult MVPShinyUsers(int topUsers)
        {

            List<MVPShiny> userShinyList = _leaderboard.TopShinyTotal(topUsers);

            if (userShinyList == null)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, userShinyList);
            }
        }



    }
}
