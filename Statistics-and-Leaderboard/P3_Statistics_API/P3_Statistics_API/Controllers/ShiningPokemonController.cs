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



        /// <summary>
        /// Display total cards of unique pokemons a user have
        /// </summary>
        /// <param name="topUsers"></param>
        /// <returns></returns>
        [HttpGet("[action]/{topUsers}")]
        public IActionResult UsersTotalCollection(int topUsers)
        {

            List<UsersCollection> userCollectionQty = _leaderboard.GetUserTotalCollection(topUsers);

            if (userCollectionQty == null)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, userCollectionQty);
            }
        }



        /// <summary>
        /// Display the total amount of unique pokemons in the world from the DB
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetTotalPokemon()
        {

            int total_pokemon = _leaderboard.GetTotalPokemon();

            if (total_pokemon == 0)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, total_pokemon);
            }
        }



        /// <summary>
        ///  Display the total amount of cards a user have
        /// </summary>
        /// <param name="topUser"></param>
        /// <returns></returns>
        [HttpGet("[action]/{topUser}")]
        public IActionResult GetTotalCardUserHave(int topUser)
        {
            List<UsersCollection> userCardAmount = _leaderboard.GetUserTotalAmount(topUser);

            if (userCardAmount == null)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, userCardAmount);
            }

        }

    }
}
