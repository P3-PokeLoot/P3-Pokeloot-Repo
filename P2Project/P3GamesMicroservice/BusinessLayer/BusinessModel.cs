using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private DataContext _context;

        public BusinessModel (DataContext context)
        {
            _context = context;
        }

        public BusinessModel()
        {
            _context = new DataContext();
        }

        /// <summary>
        /// Creates a game object for Who's that Pokemon and returns data for a new game.
        /// </summary>
        /// <returns>Game object that holds data for Who's that Pokemon</returns>
        public string WhosThatPokemonGame()
        {
            string gameObject = "";
            int numOptions = 4;
            string pictureUrl, correctPokemon;
            string[] options = new string[numOptions];

            // get the pokemon to guess
            dynamic temp = JsonConvert.DeserializeObject(RandomPokemon());
            pictureUrl = temp.sprites.front_default;
            correctPokemon = temp.name;
            // first option is correct name to start
            options[0] = correctPokemon; 

            // get random options
            for (int i = 1; i < numOptions - 1; i++) // numOptions - 1 for last item to be pikachu
            {
                while(options[i] == null)
                {
                    temp = JsonConvert.DeserializeObject(RandomPokemon());
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
        public string RandomPokemon()
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
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        /// <summary>
        /// Updates a user's rps game record with a win or creates a new record if one does not already exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating whether the update was successful (true) or not (false)</returns>
        public bool RpsWin(int userId)
        {
            bool success = false;
            try
            {
                var record = _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefault();
                if (record != null) // record exists for user
                {
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    _context.RpsgameStats.Update(record);
                    _context.SaveChanges();
                    success = true;
                }
                else // no record for user
                {
                    RpsgameStat newRecord = new RpsgameStat();
                    newRecord.UserId = userId;
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    _context.RpsgameStats.Add(newRecord);
                    _context.SaveChanges();
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
        public bool RpsLose(int userId)
        {
            bool success = false;
            try
            {
                var record = _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefault();
                if (record != null) // record exists for user
                {
                    record.TotalGamesPlayed += 1;
                    _context.RpsgameStats.Update(record);
                    _context.SaveChanges();
                    success = true;
                }
                else // no record for user
                {
                    RpsgameStat newRecord = new RpsgameStat();
                    newRecord.UserId = userId;
                    newRecord.TotalGamesPlayed = 1;
                    _context.RpsgameStats.Add(newRecord);
                    _context.SaveChanges();
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
        public bool WtpWin(int userId)
        {
            bool success = false;
            try
            {
                var record = _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefault();
                if (record != null) // record exists for user
                {
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    _context.WtpgameStats.Update(record);
                    _context.SaveChanges();
                    success = true;
                }
                else // no record for user
                {
                    WtpgameStat newRecord = new WtpgameStat();
                    newRecord.UserId = userId;
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    _context.WtpgameStats.Add(newRecord);
                    _context.SaveChanges();
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
        public bool WtpLose(int userId)
        {
            bool success = false;
            try
            {
                var record = _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefault();
                if (record != null) // record exists for user
                {
                    record.TotalGamesPlayed += 1;
                    _context.WtpgameStats.Update(record);
                    _context.SaveChanges();
                    success = true;
                }
                else // no record for user
                {
                    WtpgameStat newRecord = new WtpgameStat();
                    newRecord.UserId = userId;
                    newRecord.TotalGamesPlayed = 1;
                    _context.WtpgameStats.Add(newRecord);
                    _context.SaveChanges();
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
        public bool CapWin(int userId)
        {
            bool success = false;
            try
            {
                var record = _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefault();
                if (record != null) // record exists for user
                {
                    record.GamesWon += 1;
                    record.TotalGamesPlayed += 1;
                    _context.CapgameStats.Update(record);
                    _context.SaveChanges();
                    success = true;
                }
                else // no record for user
                {
                    CapgameStat newRecord = new CapgameStat();
                    newRecord.UserId = userId;
                    newRecord.GamesWon = 1;
                    newRecord.TotalGamesPlayed = 1;
                    _context.CapgameStats.Add(newRecord);
                    _context.SaveChanges();
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
        public bool CapLose(int userId)
        {
            bool success = false;
            try
            {
                var record = _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefault();
                if (record != null) // record exists for user
                {
                    record.TotalGamesPlayed += 1;
                    _context.CapgameStats.Update(record);
                    _context.SaveChanges();
                    success = true;
                }
                else // no record for user
                {
                    CapgameStat newRecord = new CapgameStat();
                    newRecord.UserId = userId;
                    newRecord.TotalGamesPlayed = 1;
                    _context.CapgameStats.Add(newRecord);
                    _context.SaveChanges();
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
        public string RpsRecord(int userId)
        {
            string winRecord = null;
            try
            {
                var record = _context.RpsgameStats.Where(x => x.UserId == userId).FirstOrDefault();
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
        public string WtpRecord(int userId)
        {
            string winRecord = null;
            try
            {
                var record = _context.WtpgameStats.Where(x => x.UserId == userId).FirstOrDefault();
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
        public string CapRecord(int userId)
        {
            string winRecord = null;
            try
            {
                var record = _context.CapgameStats.Where(x => x.UserId == userId).FirstOrDefault();
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
        /// Create a game description
        /// </summary>
        /// <param name="gameDetail"></param>
        /// <returns>GameInfo if successfully created otherwise null</returns>
        public GameInfo CreateGame(GameDetail gameDetail)
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
                _context.SaveChanges();

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

        public List<GameDetail> GetGameInfoList()
        {
            List<GameInfo> gameInfos;
            List<GameDetail> gameDetails = new();

            gameInfos = _context.GameInfos.ToList();

            foreach (GameInfo game in gameInfos)
            {
                GameDetail gamedetail = new()
                {
                    Id = game.Id,
                    Title = game.Title,
                    Description = game.Description,
                    ImageName = game.ImagePath,
                    Route = game.Route,
                    ImageSource = String.Format("https://localhost:44301/images/{0}", game.ImagePath)
                };

                gameDetails.Add(gamedetail);
            }

            return gameDetails;
        }

        public GameInfo DeleteGame(int id)
        {
            GameInfo gameInfo = _context.GameInfos.SingleOrDefault(game => game.Id == id);

            try
            {
                _context.GameInfos.Remove(gameInfo);
                _context.SaveChanges();
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

        public GameInfo ModifyGame(GameDetail gameDetail)
        {
            GameInfo gameInfo;

            try
            {
                gameInfo = _context.GameInfos.Single(game => game.Id == gameDetail.Id);

                gameInfo.Description = gameDetail.Description;
                gameInfo.ImagePath = gameDetail.ImageName;
                gameInfo.Route = gameDetail.Route;
                gameInfo.Title = gameDetail.Title;

                _context.SaveChanges();

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

        public GameDetail SingleGame(int id)
        {
            GameDetail gamedetail;
            try
            {
                GameInfo gameInfo = _context.GameInfos.SingleOrDefault(game => game.Id == id);

                if(gameInfo != null)
                {
                    gamedetail = new()
                    {
                        Id = gameInfo.Id,
                        Title = gameInfo.Title,
                        Description = gameInfo.Description,
                        ImageName = gameInfo.ImagePath,
                        Route = gameInfo.Route,
                        ImageSource = String.Format("https://localhost:44301/images/{0}", gameInfo.ImagePath)
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
