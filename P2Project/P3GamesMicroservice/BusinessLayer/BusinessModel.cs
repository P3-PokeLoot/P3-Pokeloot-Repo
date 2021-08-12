using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using P3Database;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessModel : IBusinessModel
    {
        private readonly DataContext _context;
        private readonly ILogger<BusinessModel> _logger;
        private readonly string _pokeApi = "https://pokeapi.com/api/v2/pokemon/";

        public BusinessModel(DataContext context, ILogger<BusinessModel> logger)
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
            }
            catch (Exception e)
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
                catch (Exception e)
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
            int numOptions = 4; // number of options to be available including correct answer
            string pictureUrl, correctPokemon;
            string[] options = new string[numOptions]; // array to hold options for shuffling later

            // get the pokemon to guess
            dynamic temp = JsonConvert.DeserializeObject(await RandomPokemonAsync());
            pictureUrl = temp.sprites.front_default;
            correctPokemon = temp.name;
            // add correct name to options array
            options[0] = correctPokemon;

            // get random options
            for (int i = 1; i < numOptions - 1; i++) // numOptions - 1 for pikachu to always be an option
            {
                while (options[i] == null)
                {
                    temp = JsonConvert.DeserializeObject(await RandomPokemonAsync());
                    if (options.Any(x => x == temp.name.ToString()) == false && temp.name.ToString() != "pikachu") // if name not already an option and not pikachu, because pikachu always option
                        options[i] = temp.name;
                }
            }
            options[numOptions - 1] = "pikachu"; // pikachu always an option

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
            int id = rand.Next(1, 899); // 898 total pokemon
            // make http request
            var client = new RestClient(_pokeApi + id);
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
                // search database for record for user
                var record = await _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to wins and games played
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    // update the database
                    _context.RpsgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    RpsgameStat newRecord = new RpsgameStat();
                    newRecord.UserId = userId;
                    // new record wins and gamesplayed will be 1
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    // add to database
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
                // check database for record with user
                var record = await _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to games played
                    record.TotalGamesPlayed += 1;
                    // update record in database
                    _context.RpsgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record
                    RpsgameStat newRecord = new RpsgameStat();
                    newRecord.UserId = userId;
                    // games played is 1 for a new record
                    newRecord.TotalGamesPlayed = 1;
                    newRecord.GamesWon = 0;
                    // add to database
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
                // check database for record for user
                var record = await _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to wins and games played
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    // update database
                    _context.WtpgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    WtpgameStat newRecord = new WtpgameStat();
                    newRecord.UserId = userId;
                    // wins and games played will be 1 for a new record
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    // add to database
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
                // check database for record for user
                var record = await _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to games played
                    record.TotalGamesPlayed += 1;
                    // update record
                    _context.WtpgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    WtpgameStat newRecord = new WtpgameStat();
                    newRecord.UserId = userId;
                    // games played is 1 for new record
                    newRecord.TotalGamesPlayed = 1;
                    newRecord.GamesWon = 0;
                    // add to database
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
                // check database for record for user
                var record = await _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to wins and games played
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    // update record
                    _context.CapgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    CapgameStat newRecord = new CapgameStat();
                    newRecord.UserId = userId;
                    // wins and games played is 1 for a new record
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    // add to database
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
                // check database for record for user
                var record = await _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to games played
                    record.TotalGamesPlayed += 1;
                    // update record
                    _context.CapgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record
                    CapgameStat newRecord = new CapgameStat();
                    newRecord.UserId = userId;
                    // games played is 1 for a new record
                    newRecord.TotalGamesPlayed = 1;
                    newRecord.GamesWon = 0;
                    // add to database
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
                // check database for record for user
                var record = await _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // if record exists for user
                {
                    // prepare string to return
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
                // check database for record for user
                var record = await _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // if record exists for user
                {
                    // prepare string to return
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
                // check database for record for user
                var record = await _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // if record exists for user
                {
                    // prepare string to return
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
        /// Retrieves a user's record for the wam game
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A string of the highest score from the WAM game</returns>
        public async Task<string> WamHighScoreAsync(int userId)
        {
            string winRecord = null;
            try
            {
                // check database for record for user
                var record = await _context.WamgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // if record exist for user
                {
                    winRecord = $"{record.HighScore}";
                }
                return winRecord;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Warning, e.Message);
            }

            return winRecord;
        }

        /// <summary>
        /// Updates a user's wam game record with their score and will record it if it is their highest score
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public async Task<bool> WamPlayedAsync(int userId, int score)
        {
            bool success = false;
            try
            {
                // check database for record for user
                var record = await _context.WamgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // check if score is higher than recorded high score
                    if (record.HighScore < score)
                        record.HighScore = score; // change high score to new score if higher
                    // add to games played
                    record.TotalGamesPlayed += 1;
                    // update record
                    _context.WamgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    WamgameStat newRecord = new WamgameStat();
                    newRecord.UserId = userId;
                    // only score is high score
                    newRecord.HighScore = score;
                    // games played is 1 for new record
                    newRecord.TotalGamesPlayed = 1;
                    // add to database
                    _context.WamgameStats.Add(newRecord);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                return success;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return success;
        }

        public async Task<bool> PhmWinAsync(int userId)
        {
            bool success = false;
            try
            {
                // check database for record for user
                var record = await _context.PhmgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to games won
                    record.GamesWon += 1;
                    // update database
                    _context.PhmgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    PhmgameStat newRecord = new PhmgameStat();
                    newRecord.UserId = userId;
                    // games won is 1 for new record
                    newRecord.GamesWon = 1;
                    // add to database
                    _context.PhmgameStats.Add(newRecord);
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
        public async Task<bool> PhmPlayedAsync(int userId)
        {
            bool success = false;
            try
            {
                // check database for record for user
                var record = await _context.PhmgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // add to games played
                    record.TotalGamesPlayed += 1;
                    // update database
                    _context.PhmgameStats.Update(record);
                    await _context.SaveChangesAsync();
                    success = true;
                }
                else // no record for user
                {
                    // create new record for user
                    PhmgameStat newRecord = new PhmgameStat();
                    newRecord.UserId = userId;
                    // games played is 1 for new record
                    newRecord.TotalGamesPlayed = 1;
                    // add to database
                    _context.PhmgameStats.Add(newRecord);
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

        public async Task<string> PhmRecordAsync(int userId)
        {
            string winRecord = null;
            try
            {
                // check database for record for user
                var record = await _context.PhmgameStats.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (record != null) // record exists for user
                {
                    // prepare string to return
                    winRecord = $"{record.GamesWon}/{record.TotalGamesPlayed}";
                }
                return winRecord;
            }
            catch
            {
                return winRecord;
            }
        }

        /// Create a game description
        /// </summary>
        /// <param name="gameDetail"></param>
        /// <returns>GameInfo if successfully created otherwise null</returns>
        public async Task<GameInfo> CreateGameAsync(GameDetail gameDetail)
        {

            GameInfo gameInfo = new()
            {
                Title = gameDetail.Title,
                Description = gameDetail.Description,
                ImagePath = gameDetail.ImageName,
                Route = gameDetail.Route
            };

            try
            {
                _context.GameInfos.Add(gameInfo);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {

                return null;
            }
            catch (DbUpdateException)
            {

                return null;
            }

            return gameInfo;

        }

        public async Task<List<GameDetail>> GetGameInfoListAsync()
        {
            List<GameInfo> gameInfos;
            List<GameDetail> gameDetails = new();

            gameInfos = await _context.GameInfos.ToListAsync();

            foreach (GameInfo game in gameInfos)
            {
                GameDetail gamedetail = new()
                {
                    Id = game.Id,
                    Title = game.Title,
                    Description = game.Description,
                    ImageName = game.ImagePath,
                    Route = game.Route,
                    ImageSource = String.Format("https://p3pokeloot.com/api/Games/images/{0}", game.ImagePath)
                };

                gameDetails.Add(gamedetail);
            }

            return gameDetails;
        }

        public async Task<GameInfo> DeleteGameAsync(int id)
        {
            GameInfo gameInfo = _context.GameInfos.SingleOrDefault(game => game.Id == id);

            try
            {
                _context.GameInfos.Remove(gameInfo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return null;
            }
            catch (DbUpdateException)
            {

                return null;
            }
            return gameInfo;
        }

        public async Task<GameInfo> ModifyGameAsync(GameDetail gameDetail)
        {
            GameInfo gameInfo;

            try
            {
                gameInfo = _context.GameInfos.Single(game => game.Id == gameDetail.Id);

                gameInfo.Description = gameDetail.Description;
                gameInfo.ImagePath = gameDetail.ImageName;
                gameInfo.Route = gameDetail.Route;
                gameInfo.Title = gameDetail.Title;

                await _context.SaveChangesAsync();

            }
            catch (InvalidOperationException)
            {

                return null;
            }
            catch (DbUpdateConcurrencyException)
            {

                return null;
            }

            return gameInfo;

        }

        public async Task<GameDetail> SingleGameAsync(int id)
        {
            GameDetail gamedetail;
            try
            {
                GameInfo gameInfo = await _context.GameInfos.Where(game => game.Id == id).FirstOrDefaultAsync();

                if (gameInfo != null)
                {
                    gamedetail = new()
                    {
                        Id = gameInfo.Id,
                        Title = gameInfo.Title,
                        Description = gameInfo.Description,
                        ImageName = gameInfo.ImagePath,
                        Route = gameInfo.Route,
                        ImageSource = String.Format("https://p3pokeloot.com/api/Games/images/{0}", gameInfo.ImagePath)
                    };
                    return gamedetail;
                }
                else
                {
                    return null;
                }

            }
            catch (InvalidOperationException)
            {

                return null;
            }
            catch (ArgumentNullException)
            {

                return null;
            }
        }
    }
}
