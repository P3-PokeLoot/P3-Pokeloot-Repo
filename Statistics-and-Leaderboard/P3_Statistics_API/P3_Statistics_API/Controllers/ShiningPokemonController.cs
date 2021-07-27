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


        [HttpGet("TopTenShinyUsers")]
        public IActionResult TopTenShinyUsers()
        {
            string error;
            List<CardCollection> userShinyList = _leaderboard.TopTenShinyUsers();

            if (userShinyList == null)
            {
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, userShinyList);
            }
        }










        // GET: api/<ShiningPokemonController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShiningPokemonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShiningPokemonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShiningPokemonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShiningPokemonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
