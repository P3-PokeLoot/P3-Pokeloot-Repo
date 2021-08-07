using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using P3Database;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessModel : IBusinessModel
    {
        private readonly DataContext _context;
        private readonly ILogger<BusinessModel> _logger;

        public BusinessModel (DataContext context, ILogger<BusinessModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BusinessModel()
        {
            _context = new DataContext();
        }


        /// <summary>
        /// Returns game info list from db context
        /// </summary>
        /// <returns>List of GameInfo objects or null if error occured</returns>
        public async Task<List<GameInfo>> GameInfoListAsync()
        {
            List<GameInfo> gameInfos = null;
            try
            {
                gameInfos = await _context.GameInfos.ToListAsync();
            }catch(Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                gameInfos = null;
            }
            return gameInfos;
        }

        /// <summary>
        /// Adds a GameInfo object to the game info list
        /// </summary>
        /// <param name="gameInfo">Game info to add to db</param>
        /// <returns>Boolean indicating whether the addition was successful (true) or not (false)</returns>
        public async Task<bool> AddGameInfoAsync(GameInfo gameInfo)
        {
            //Check if incoming gameInfo object is not null
            if (gameInfo != null)
            {
                try 
                {
                    //Add to dbcontext and save changes
                    _context.GameInfos.Add(gameInfo);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception e)
                {
                    //Log error if one occurs
                    _logger.Log(LogLevel.Error, e.Message);
                }
            }

            //return false if unsuccessful
            return false;
        }

        /// <summary>
        /// Creates a game object for Who's that Pokemon and returns data for a new game.
        /// </summary>
        /// <returns>Game object that holds data for Who's that Pokemon</returns>
        public async Task<string> WhosThatPokemonGameAsync()
        {
            string gameObject = "";
            int numOptions = 4;
            string pictureUrl, correctPokemon;
            string[] options = new string[numOptions];

            // get the pokemon to guess
            dynamic temp = JsonConvert.DeserializeObject(await RandomPokemonAsync());
            pictureUrl = temp.sprites.front_default;
            correctPokemon = temp.name;
            // first option is correct name to start
            options[0] = correctPokemon; 

            // get random options
            for (int i = 1; i < numOptions - 1; i++) // numOptions - 1 for last item to be pikachu
            {
                while(options[i] == null)
                {
                    temp = JsonConvert.DeserializeObject(await RandomPokemonAsync());
                    if(options.Where(x => x == temp.name.ToString()).FirstOrDefault() == null && temp.name.ToString() != "pikachu") // if name not already an option and not pikachu, because pikachu always option
                        options[i] = temp.name;
                }
            }
            options[numOptions - 1] = "pikachu"; // pikachu always an option for the lolz

            // shuffle options array
            Random rand = new Random();
            options = options.OrderBy(x => rand.Next()).ToArray();

            // make an object to return
            WtpGame wtpGame = new WtpGame(pictureUrl, correctPokemon, options);
            gameObject = JsonConvert.SerializeObject(wtpGame);
            return gameObject;
        }

        /// <summary>
        /// Calls Poke API and gets a random Pokemon details.
        /// </summary>
        /// <returns>Serialized Pokemon data for a random Pokemon.</returns>
        public async Task<string> RandomPokemonAsync()
        {
            // get random number for pokemon id
            var rand = new Random();
            int id = rand.Next(1, 899); // 898 total pokemon?
            // make http request
            var client = new RestClient("https://pokeapi.co/api/v2/pokemon/"+id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }

        /// <summary>
        /// Updates a user's rps game record with a win or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> RpsWinAsync(int userId)
        {
            bool success = false;
            try
            {
                var record = await _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    _context.RpsgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    RpsgameStat newRecord = new RpsgameStat();
                    newRecord.UserId = userId;
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    _context.RpsgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch
            {
                return success;
            }
        }

        /// <summary>
        /// Updates a user's rps game record or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> RpsLoseAsync(int userId)
        {
            bool success = false;
            try
            {
                var record = await _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    record.TotalGamesPlayed += 1;
                    _context.RpsgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    RpsgameStat newRecord = new RpsgameStat();
                    newRecord.UserId = userId;
                    newRecord.TotalGamesPlayed = 1;
                    _context.RpsgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch
            {
                return success;
            }
        }

        /// <summary>
        /// Updates a user's wtp game record with a win or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> WtpWinAsync(int userId)
        {
            bool success = false;
            try
            {
                var record = await _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    _context.WtpgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    WtpgameStat newRecord = new WtpgameStat();
                    newRecord.UserId = userId;
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    _context.WtpgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch
            {
                return success;
            }
        }

        /// <summary>
        /// Updates a user's wtp game record or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> WtpLoseAsync(int userId)
        {
            bool success = false;
            try
            {
                var record = await _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    record.TotalGamesPlayed += 1;
                    _context.WtpgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    WtpgameStat newRecord = new WtpgameStat();
                    newRecord.UserId = userId;
                    newRecord.TotalGamesPlayed = 1;
                    _context.WtpgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch
            {
                return success;
            }
        }

        /// <summary>
        /// Updates a user's cap game record with a win or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> CapWinAsync(int userId)
        {
            bool success = false;
            try
            {
                var record = await _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    _context.CapgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    CapgameStat newRecord = new CapgameStat();
                    newRecord.UserId = userId;
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    _context.CapgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch
            {
                return success;
            }
        }

        /// <summary>
        /// Updates a user's cap game record or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> CapLoseAsync(int userId)
        {
            bool success = false;
            try
            {
                var record = await _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    record.TotalGamesPlayed += 1;
                    _context.CapgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    CapgameStat newRecord = new CapgameStat();
                    newRecord.UserId = userId;
                    newRecord.TotalGamesPlayed = 1;
                    _context.CapgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch
            {
                return success;
            }
        }

        /// <summary>
        /// Retrieves a user's record for the rps game
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A string of the number of games a user has won out of the total number of games played (wins/total games)</returns>
        public async Task<string> RpsRecordAsync(int userId)
        {
            string winRecord = null;
            try
            {
                var record = await _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null)
                {
                    winRecord = $"{record.GamesWon}/{record.TotalGamesPlayed}";
                }
                return winRecord;
            }
            catch
            {
                return winRecord;
            }
        }

        /// <summary>
        /// Retrieves a user's record for the wtp game
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A string of the number of games a user has won out of the total number of games played (wins/total games)</returns>
        public async Task<string> WtpRecordAsync(int userId)
        {
            string winRecord = null;
            try
            {
                var record = await _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null)
                {
                    winRecord = $"{record.GamesWon}/{record.TotalGamesPlayed}";
                }
                return winRecord;
            }
            catch
            {
                return winRecord;
            }
        }

        /// <summary>
        /// Retrieves a user's record for the cap game
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A string of the number of games a user has won out of the total number of games played (wins/total games)</returns>
        public async Task<string> CapRecordAsync(int userId)
        {
            string winRecord = null;
            try
            {
                var record = await _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null)
                {
                    winRecord = $"{record.GamesWon}/{record.TotalGamesPlayed}";
                }
                return winRecord;
            }
            catch
            {
                return winRecord;
            }
        }
    }
}
