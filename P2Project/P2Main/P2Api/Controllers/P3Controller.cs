
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
    public class P3Controller : Controller
    {
        private readonly ILogger<P3Controller> _logger;

        private readonly IBusinessModel _businessModel;


        /// <summary>
        /// Constructor to inject the business layer
        /// </summary>
        /// <param name="businessModel">Business model object</param>
        /// <param name="logger">Logger object</param>
        public P3Controller(IBusinessModel businessModel, ILogger<P3Controller> logger)
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
            foreach (Post post in playerList)
            {
                DisplayBoard displayBoard = _businessModel.getPostInfo(post.PostId);
                string mainSprite = "https://wiki.p-insurgence.com/images/0/09/722.png"; //if a discussion post, use default image
                string cardName = "";
                int cardRare = 0;
                if (post.PokemonId != null) //checks if it is sale or display post
                {
                    if (post.IsShiny == true) //checks with image to use
                    {
                        mainSprite = _businessModel.getPokemonById((int)post.PokemonId).SpriteLinkShiny;
                    }
                    else
                    {
                        mainSprite = _businessModel.getPokemonById((int)post.PokemonId).SpriteLink;
                    }
                    cardName = _businessModel.getPokemonById((int)post.PokemonId).PokemonName;
                    cardRare = _businessModel.getPokemonById((int)post.PokemonId).RarityId;
                }

                FullPost instance = new FullPost()//creates full post object to be displayed
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
        public string buyCard(int postId, int userId)
        {
            
            Post post = _businessModel.getPostById(postId);
            User currentUser = _businessModel.GetUserById(userId);
            Dictionary<string, bool> result = _businessModel.buyFromPost(post, currentUser);
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
        /// https://localhost:44307/api/P2/Lootbox/2/1
        /// Gets a random pokemon and adds it the the users collection with the id passed in
        /// </summary>
        /// <param name="userId">user id for user rolling lootbox</param>
        /// <param name="boxType">type of box to roll</param>
        /// <returns>Serialized string of dict containing PokemonCard object of the random choice and shiny boolean</returns>
        [HttpGet("Lootbox/{userId}/{boxType}")]
        //public Dictionary<PokemonCard, bool> Lootbox(int userId)
        public string Lootbox(int userId, int boxType)
        {
            User currentUser = _businessModel.GetUserById(userId);
            Dictionary<PokemonCard, bool> newCard;

            //check boxtype for each case
            if (boxType == 1)
            {
                const int lootBoxCost = 100;
                _businessModel.incrementUserBalance(currentUser, -lootBoxCost);
                newCard = _businessModel.rollLootbox(currentUser, 1);
            }
            else if (boxType == 2)
            {
                const int lootBoxCost = 500;
                _businessModel.incrementUserBalance(currentUser, -lootBoxCost);
                newCard = _businessModel.rollLootbox(currentUser, 2);
            }
            else
            {
                const int lootBoxCost = 1000;
                _businessModel.incrementUserBalance(currentUser, -lootBoxCost);
                newCard = _businessModel.rollLootbox(currentUser, 3);
            }
            
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
        /// https://localhost:44307/api/P2/Signup
        /// Gets all the pokemon objects and their quanity in realtion to the input userId
        /// </summary>
        /// <param name="userId">id of desired users collection</param>
        /// <returns>Serialized string of dict containing PokemonCard object and its relation to the users collection for quanities</returns>
        [HttpPost("Signup")]
        public ActionResult Signup(User userObj)
        {
            bool isCreated = _businessModel.signUp(userObj);

            if (isCreated)
            {
                return CreatedAtAction("Signup", new { name = userObj.FirstName, ok = true }, new { ok = true, newUser = userObj });
            }

            return BadRequest(new { message = "Error User already Exist", status = -1 });
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

       
        /// <summary>
        /// Returns User object by Id
        /// https://localhost:44307/api/P2/Balance/2
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
        /// https://localhost:44307/api/P2/newPost/150/true/3/description
        /// Creates a new Post
        /// </summary>
        /// <param name="userId">id of desired users collection</param>
        /// <returns>A status code back to the user</returns>
        [HttpGet("Post/{pokemonId}/{postPrice}/{isShiny}/{userId}/{descr}")]
        public bool newPost(int pokemonId, int postPrice, bool isShiny, int userId, string descr)
        {
            User currentUser = _businessModel.GetUserById(userId);
            Post post = new() //creates new post object from info
            {
                PokemonId = pokemonId == 0 ? null : pokemonId, //if id is 0, set it to null
                PostDescription = descr,
                Price = postPrice == 0 ? null : postPrice, //if price is 0, set it to null
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

        ///<summary> 
        /// returns list of rarity type objects from Db
        /// https://localhost:44307/api/P2/RarityTypes
        /// </summary>
        /// <returns>List of rarity type objects</returns>
        [HttpGet("RarityTypes")]
        public List<RarityType> RarityTypes()
        {
            return _businessModel.GetRarityTypes();
        }


        /// <summary>
        /// adds a specified number of coins to the users account
        /// https://localhost:44307/api/P2/EarnCoins/2/100
        /// </summary>
        /// <param name="userId">user to add coins to</param>
        /// <param name="coinsAmount">amount of coins to add</param>
        /// <returns>New coin balance</returns>
        [HttpGet("EarnCoins/{userId}/{coinsAmount}")]
        public int EarnCoins(int userId, int coinsAmount)
        {
            User currentUser = _businessModel.GetUserById(userId);
            _businessModel.incrementUserBalance(currentUser, coinsAmount);
            return currentUser.CoinBalance;
        }

        /// <summary>
        /// Removes a post from displayboard
        /// https://localhost:44307/api/P2/RemovePost/54
        /// </summary>
        /// <param name="postID">PostID</param>
        /// <returns>if successful removal</returns>
        [HttpGet("RemovePost/{idpost}")]
        public bool RemovePost(int idpost)
        {
            bool result = _businessModel.hidePost(idpost);
            return result;
        }

        /// <summary>
        /// edits price of post
        /// https://localhost:44307/api/P2/EditPrice/54/100
        /// </summary>
        /// <param name="postID">PostID</param>
        /// <param name="newPrice">new price</param>
        /// <returns>if successful edits</returns>
        [HttpGet("EditPrice/{idpost}/{newPrice}")]
        public bool EditPrice(int idpost, int newPrice)
        {
            bool result = _businessModel.editPrice(idpost, newPrice);
            return result;
        }

        /// <summary>
        /// Adds a new PostComment object to the database
        /// </summary>
        /// <param name="userId">The user id of the user creating the comment</param>
        /// <param name="postId">The id to the post the comment is created on</param>
        /// <param name="content">The contents of the comment being created</param>
        /// <returns>true or false based on creation status</returns>
        [HttpGet("PostComment/{userId}/{postId}/{content}")]
        public bool PostComment(int userId, int postId, string content)
        {
            bool isCreated = _businessModel.newPostComment(userId, postId, content);
            return isCreated;
        }


        //[HttpPost("PostComment/{newComment}")]
        //public bool PostComment(string newComment)
        //{
        //    bool isCreated = true;// _businessModel.newPostComment(userId, postId, content);
        //    return isCreated;
        //}

        [HttpGet("Comments/{postId}")]
        public string Comments(int postId)
        {
            Dictionary<PostComment, string> commentDict = _businessModel.getCommentList(postId);
            string json = JsonConvert.SerializeObject(commentDict.ToList());
            return json;
        }



        [HttpGet("FullPostById/{postId}")]
        public FullPost FullPost(int postId)
        {
            Post currentPost = _businessModel.getPostById(postId);
            DisplayBoard curretntDisplayBoard = _businessModel.getPostInfo(postId);


            string mainSprite = "https://wiki.p-insurgence.com/images/0/09/722.png";
            string cardName = "";
            int cardRare = 0;

            if (currentPost != null)
            {
                if (currentPost.IsShiny == true && currentPost.PokemonId != null)
                {
                    mainSprite = _businessModel.getPokemonById((int)currentPost.PokemonId).SpriteLinkShiny;
                }
                else if (currentPost.PokemonId != null)
                {
                    mainSprite = _businessModel.getPokemonById((int)currentPost.PokemonId).SpriteLink;
                }
                if(currentPost.PokemonId != null)
                {
                    cardName = _businessModel.getPokemonById((int)currentPost.PokemonId).PokemonName;
                    cardRare = _businessModel.getPokemonById((int)currentPost.PokemonId).RarityId;
                }
                else
                {
                    cardName = "";
                    cardRare = 0;
                }
                
            }

            FullPost currentFullPost = new FullPost()
            {
                PostId = currentPost.PostId,
                PokemonId = currentPost.PokemonId,
                PostTime = currentPost.PostTime,
                PostDescription = currentPost.PostDescription,
                Price = currentPost.Price,
                StillAvailable = currentPost.StillAvailable,
                IsShiny = currentPost.IsShiny,
                UserId = curretntDisplayBoard.UserId,
                PostType = curretntDisplayBoard.PostType,
                PokemonName = cardName,
                RarityId = cardRare,
                UserName = _businessModel.GetUserById(curretntDisplayBoard.UserId).UserName,
                SpriteLink = mainSprite

            };
            return currentFullPost;
        }


        /// Changes favorite status of a card in your collection
        /// https://localhost:44307/api/P2/Favorite/3/151
        /// </summary>
        /// <param name="UserId">Current User ID</param>
        /// <param name="Poke">Pokemon card id</param>
        /// <returns>If action was successful</returns>
        [HttpGet("Favorite/{UserId}/{Poke}")]
        public bool Favorite(int UserId, int Poke)
        {
            bool result = _businessModel.favoriteCard(UserId, Poke);
            return result;
        }

        /// <summary>
        /// Gets friends list of a specific user
        /// https://localhost:44307/api/P2/Friends/2
        /// </summary>
        /// <param name="UserId">Current User ID</param>
        /// <returns>List of friends</returns>
        [HttpGet("Friends/{UserId}")]
        public List<FullFriend> Friends(int UserId)
        {
            List<FullFriend> result = _businessModel.GetFriends(UserId);
            return result;
        }


        /// <summary>
        /// Does various actions based on friendship status between two users
        /// https://localhost:44307/api/P2/FriendAction/2/1
        /// </summary>
        /// <param name="UserId">Current User Id</param>
        /// <param name="FriendId">Intended friend id</param>
        /// <returns>Outputs a string with information on friendship status and if anything has changed</returns>
        [HttpGet("FriendAction/{UserId}/{FriendId}")]
        public string FriendAction(int UserId, int FriendId)
        {
            string result = _businessModel.friendAction(UserId, FriendId);
            return result;
        }

        /// <summary>
        /// Gets a specific user by Id
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A MessageUser object representing the desired user that contains necessary messaging information</returns>
        [HttpGet("[action]/{userId}")]
        public MessageUser GetMessageUser(int userId)
        {
            return _businessModel.GetMessageUserById(userId);
        }

        /// <summary>
        /// Gets a specific user by username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>A MessageUser object representing the desired user that contains necessary messaging information</returns>
        [HttpGet("[action]/{username}")]
        public MessageUser GetMessageUserByUsername(string username)
        {
            return _businessModel.GetMessageUserByUsername(username);
        }

        [HttpGet("[action]/{senderId}/{receiverId}")]
        public IEnumerable<Message> GetMessages(int senderId, int receiverId)
        {
            return _businessModel.GetMessagesBetween(senderId, receiverId);
        }

        [HttpDelete("[action]/{user1Id}/{user2Id}")]
        public bool DeleteMessagesBetween(int user1Id, int user2Id)
        {
            return _businessModel.DeleteMessagesBetween(user1Id, user2Id);
        }

        [HttpPost("[action]")]
        public void PostMessage([FromBody] Message newMessage)
        {
            if (ModelState.IsValid)
            {
                _businessModel.PostMessage(newMessage);
            }
        }

        [HttpGet("[action]/{userId}")]
        public IEnumerable<User> GetOngoingMessages(int userId)
        {
            return _businessModel.GetOngoingConversationUsers(userId);
        }
    } // end class
} // end namespace

