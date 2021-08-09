using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisinessLayerMethods;
using RepositoryModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P3_Statistics_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Coins_Achievement : ControllerBase
    {


        private readonly IArchievement_Coins _archievement_Coins;

        public Coins_Achievement(IArchievement_Coins archievement_Coins)
        {
            _archievement_Coins = archievement_Coins;
        }




        /// <summary>
        /// Save user archievement of earn 100 coin in the UserAchivement table if is completed return true, false is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userId}")]
        public IActionResult UserCoins100ArchievementComp(int userId)
        {

            bool result = _archievement_Coins.UserEarn100Coins(userId);

            if(result == true)
            {

                Console.WriteLine("Completed Archievement");
                return StatusCode(200, result);
            }
            else
            {
                Console.WriteLine("Not Completed Archievement");
                return StatusCode(200, result);
            }

        }



        /// <summary>
        /// Save user archievement of earn 1000 coin in the UserAchivement table if is completed return true, false is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userId}")]
        public IActionResult UserCoins1000ArchievementComp(int userId)
        {

            bool result = _archievement_Coins.UserEarn1000Coins(userId);

            if (result == true)
            {

                Console.WriteLine("Completed Archievement");
                return StatusCode(200, result);
            }
            else
            {
                Console.WriteLine("Not Completed Archievement");
                return StatusCode(200, result);
            }

        }



        /// <summary>
        /// Save user archievement of earn 10000 coin in the UserAchivement table if is completed return true, false is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userId}")]
        public IActionResult UserCoins10000ArchievementComp(int userId)
        {

            bool result = _archievement_Coins.UserEarn10000Coins(userId);

            if (result == true)
            {

                Console.WriteLine("Completed Archievement");
                return StatusCode(200, result);
            }
            else
            {
                Console.WriteLine("Not Completed Archievement");
                return StatusCode(200, result);
            }

        }

        /// <summary>
        /// Return a list of all hte archivement of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userId}")]
        public IActionResult UserArchievementsList(int userId)
        {

            List<Achievement> list = _archievement_Coins.UserAchievements(userId);

            if(list == null)
            {

                Console.WriteLine("List not found....");
                return StatusCode(400);
            }
            else
            {
                return StatusCode(200, list);
            }
        
        }



















            // GET: api/<Coins_Achievement>
            [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Coins_Achievement>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Coins_Achievement>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Coins_Achievement>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Coins_Achievement>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
