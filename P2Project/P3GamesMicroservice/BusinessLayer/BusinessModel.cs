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
    }
}
