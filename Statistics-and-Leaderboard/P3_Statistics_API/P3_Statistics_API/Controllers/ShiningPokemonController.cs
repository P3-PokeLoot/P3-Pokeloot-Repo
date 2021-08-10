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

        private readonly ILeaderboardBuissnes _leaderboard;

        public ShiningPokemonController(ILeaderboardBuissnes leaderboard)
        {
            _leaderboard = leaderboard;
        }

        /// <summary>
        /// Retrive the users with most shinys for pokmemon 
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
        /// Display the Sum of all shinys a user has
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
        /// Display the amount of unique pokemons a user have in the collection
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
        /// Display the total amount of unique pokemons in the database that exist
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


        /// <summary>
        /// Users can see the percentage of all users that currently own this pokemon card.
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{pokemonName}")]
        public IActionResult GetCardPorcentage(string pokemonName)
        {
            //pokemonName = pokemonName.ToLower();

            PercentageOwnCard cardporcentage = _leaderboard.GetPercentageOwnCard(pokemonName);


            if (pokemonName == null)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, cardporcentage);
            }
        }

    }
}
