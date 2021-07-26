
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using P2DbContext.Models;
using BusinessLayer;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace P2Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class P2Controller : Controller
    {
        private readonly ILogger<P2Controller> _logger;

        private readonly IBusinessModel _businessModel;


        /// <summary>
        /// Constructor to inject the business layer
        /// </summary>
        /// <param name="businessModel">Business model object</param>
        /// <param name="logger">Logger object</param>
        public P2Controller(IBusinessModel businessModel , ILogger<P2Controller> logger)
        {
            this._businessModel = businessModel;
            this._logger = logger;
        }

        /// <summary>
        /// https://localhost:44307/api/P2/DisplayBoard
        /// Return the json object for each post in the database to build the display board
        /// </summary>
        /// <returns>List of Posts</returns>
        [HttpGet("DisplayBoard")]
        public List<FullPost> PostList()
        {
            List<FullPost> result = new List<FullPost>();
            List<Post> playerList = _businessModel.getDisplayBoard();
            foreach(Post post in playerList){
                DisplayBoard displayBoard = _businessModel.getPostInfo(post.PostId);
                string mainSprite = "https://wiki.p-insurgence.com/images/0/09/722.png";
                string cardName ="";
                int cardRare = 0;
                if(post.PokemonId != null){
                    if(post.IsShiny == true){
                        mainSprite = _businessModel.getPokemonById((int)post.PokemonId).SpriteLinkShiny;
                    }
                    else{
                        mainSprite = _businessModel.getPokemonById((int)post.PokemonId).SpriteLink;
                    }
                   cardName = _businessModel.getPokemonById((int)post.PokemonId).PokemonName;
                   cardRare = _businessModel.getPokemonById((int)post.PokemonId).RarityId;
                }
                
                FullPost instance = new FullPost()
                {
                    PostId = post.PostId,
                    PokemonId = post.PokemonId,
                    PostTime = post.PostTime,
                    PostDescription = post.PostDescription,
                    Price = post.Price,
                    StillAvailable = post.StillAvailable,
                    IsShiny = post.IsShiny,
                    UserId = displayBoard.UserId,
                    PostType = displayBoard.PostType,
                    PokemonName = cardName,
                    RarityId = cardRare,
                    UserName = _businessModel.GetUserById(displayBoard.UserId).UserName,
                    SpriteLink = mainSprite

                };
                result.Add(instance);
            }
            result.Reverse();
            return result;
        }

        /// <summary>
        /// https://localhost:44307/api/P2/buyCard/3/2
        /// Returns the result from buying a card
        /// </summary>
        /// <param name="postId">Post ID of a sale</param>
        /// <param name="userId">Current User</param>
        /// <returns>Dictionary conation output and outcome</returns>
        [HttpGet("buyCard/{postId}/{userId}")]
        public string buyCard(int postId, int userId){
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            Post post = _businessModel.getPostById(postId);
            User currentUser = _businessModel.GetUserById(userId);
            result = _businessModel.buyFromPost(post, currentUser);
            string json = JsonConvert.SerializeObject(result.ToList());
            return json;
        }

        /// <summary>
        /// https://localhost:44307/api/P2/Pokemon/2
        /// Returns the json object for a selected Pokemon
        /// </summary>
        /// <param name="id">id to select by</param>
        /// <returns>Pokemon card</returns>
        [HttpGet("Pokemon/{id}")]
        public PokemonCard Pokemon(int id)
        {
            PokemonCard selectedCard = _businessModel.getPokemonById(id);
            return selectedCard;
        }



        /// <summary>
        /// https://localhost:44307/api/P2/Login/mason.sanborn/Revature
        /// Returns the json object for the validated user or null if the user does not exist
        /// </summary>
        /// <param name="username">input username</param>
        /// <param name="password">input password</param>
        /// <returns>User object or null</returns>
        [HttpGet("Login/{username}/{password}")]
        public User Login(string username, string password)
        {
            User currentUser = _businessModel.login(username, password);
            return currentUser;
        }



        /// <summary>
        /// https://localhost:44307/api/P2/Lootbox/2
        /// Gets a random pokemon and adds it the the users collection with the id passed in
        /// </summary>
        /// <param name="userId">user id for user rolling lootbox</param>
        /// <returns>Serialized string of dict containing PokemonCard object of the random choice and shiny boolean</returns>
        [HttpGet("Lootbox/{userId}")]
        //public Dictionary<PokemonCard, bool> Lootbox(int userId)
        public string Lootbox(int userId)
        {
            const int lootBoxCost = 100;
            User currentUser = _businessModel.GetUserById(userId);
            _businessModel.incrementUserBalance(currentUser, -lootBoxCost);

            Dictionary<PokemonCard, bool> newCard = _businessModel.rollLootbox(currentUser);
            string json = JsonConvert.SerializeObject(newCard.ToList());
            return json;
        }


        /// <summary>
        /// https://localhost:44307/api/P2/UserCollection/2
        /// Gets all the pokemon objects and their quanity in realtion to the input userId
        /// </summary>
        /// <param name="userId">id of desired users collection</param>
        /// <returns>Serialized string of dict containing PokemonCard object and its relation to the users collection for quanities</returns>
        [HttpGet("UserCollection/{userId}")]
        public string UserCollection(int userId)
        {
            User currentUser = _businessModel.GetUserById(userId);
            Dictionary<CardCollection, PokemonCard> userCollection = _businessModel.getUserCollection(currentUser);
            string json = JsonConvert.SerializeObject(userCollection.ToList());
            return json;
        }

        /// <summary>
        /// https://localhost:44307/api/P2/UserProfile/2
        /// Gets an updated user object for achievment dsiplaying purposes
        /// </summary>
        /// <param name="userId">id of desired users object</param>
        /// <returns>Serialized string of currentuser Profile</returns>
        [HttpGet("Profile/{userId}")]
        public string GetUserProfile(int userId)
        {
            User currentUser = _businessModel.GetUserById(userId);
            string json = JsonConvert.SerializeObject(currentUser);
            return json;
        }

        /// <summary>
        /// Returns User object by Id
        /// </summary>
        /// <param name="userId">user id to get object for</param>
        /// <returns>User object</returns>
        //[HttpGet("CoinBalance/{userId}")]
        //public int CoinBalance(int userId)
        //{
        //    User currentUser = _businessModel.GetUserById(userId);
        //    _businessModel.incrementUserBalance(currentUser, -100);
        //    return currentUser.CoinBalance;
        //}

        /// <summary>
        /// https://localhost:44307/api/P2/Signup
        /// Gets all the pokemon objects and their quanity in realtion to the input userId
        /// </summary>
        /// <param name="userId">id of desired users collection</param>
        /// <returns>Serialized string of dict containing PokemonCard object and its relation to the users collection for quanities</returns>
        [HttpPost("Signup")]
        public ActionResult Signup(User userObj) 
        {
            bool isCreated = _businessModel.signUp(userObj);
               
            if(isCreated) 
            { 
            return CreatedAtAction("Signup", new { name = userObj.FirstName, ok = true }, new {ok = true, newUser = userObj});
            }

            return BadRequest(new { message="Error User already Exist", status=-1});
        }

        /// <summary>
        /// https://localhost:44307/api/P2/DeleteUser/18
        /// Removes a user from the database.
        /// </summary>
        /// <param name="userId">id of desired users collection</param>
        /// <returns>A status code back to the user</returns>
        [HttpDelete("DeleteUser/{userId:int}")]
        public ActionResult DeleteUser(int userId)
        {
            bool isDeleted;
            try
            {
               isDeleted = _businessModel.RemoveUser(userId);

               if (!isDeleted)
                {
                    return NotFound($"Employee with Id = {userId} not found");
                }

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        ///<summary>
        ///Returns new coin balance
        /// </summary>
        /// <param name="userId"> UserId who's balance was adjusted</param>
        /// <summary>
        /// Returns User object by Id
        /// </summary>
        /// <param name="userId">user id to get object for</param>
        /// <returns>User object</returns>
        [HttpGet("Balance/{userId}")]
        public int Balance(int userId)
        {
            User currentUser = _businessModel.GetUserById(userId);
            return currentUser.CoinBalance;
        }

        /// <summary>
        /// https://localhost:44307/api/P2/Post/Create
        /// Creates a new Post
        /// </summary>
        /// <param name="userId">id of desired users collection</param>
        /// <returns>A status code back to the user</returns>
        [HttpGet("Post/{pokemonId}/{postPrice}/{isShiny}/{userId}/{descr}")]
        public bool newPost(int pokemonId, int postPrice, bool isShiny, int userId, string descr)
        {
            User currentUser = _businessModel.GetUserById(userId);
            Post post = new()
            {
                PokemonId = pokemonId,
                PostDescription = descr,
                Price = postPrice == 0 ? null : postPrice,
                StillAvailable = true,
                IsShiny = isShiny
            };

            if (currentUser != null)
            {
                bool isCreated = _businessModel.newPost(post, currentUser);
                return isCreated;
            }
            return false;
        }

        /// returns list of rarity type objects from Db
        /// </summary>
        /// <returns>List of rarity type objects</returns>
        [HttpGet("RarityTypes")]
        public List<RarityType> RarityTypes()
        {
            return _businessModel.GetRarityTypes();
        }


        /// <summary>
        /// adds a specified number of coins to the users account
        /// </summary>
        /// <param name="userId">user to add coins to</param>
        /// <param name="coinsAmount">amount of coins to add</param>
        /// <returns></returns>
        [HttpGet("EarnCoins/{userId}/{coinsAmount}")]
        public int EarnCoins(int userId, int coinsAmount)
        {
            User currentUser = _businessModel.GetUserById(userId);
            _businessModel.incrementUserBalance(currentUser, coinsAmount);
            return currentUser.CoinBalance;
        }

        /// <summary>
        /// Removes a post
        /// </summary>
        /// <param name="postID">PostID</param>
        /// <returns>if successful removal</returns>
        [HttpGet("RemovePost/{idpost}")]
        public bool RemovePost(int idpost){
            bool result = _businessModel.hidePost(idpost);
            return result;
        }


    } // end class
} // end namespace

