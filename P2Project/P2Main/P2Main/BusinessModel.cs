using System;
using System.Collections;
using System.Collections.Generic;
using P2DbContext.Models;
using System.Linq;

namespace BusinessLayer
{
    public class BusinessModel : IBusinessModel
    {
        public P3DbClass context;

        /// <summary>
        /// Constructor for business class that takes a Db context
        /// </summary>
        /// <param name="context">Db context</param>
        public BusinessModel(P3DbClass context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor for business class that takes no parameter
        /// </summary>
        public BusinessModel()
        {
            this.context = new P3DbClass();
        }

        /// <summary>
        /// Generate a lootbox for the player will add a randomly generated card into the users collection and will update the datebase based on if the card generated is shiny or not.
        /// </summary>
        /// <param name="currentUser">Current user who is recieving a lootbox</param>
        /// <param name="boxType">Ctype of box to be rolled</param>
        /// <returns>Dictionary object where key is the generated card and the value is a boolean stating whether or not the card is shiny</returns>
        public Dictionary<PokemonCard, bool> rollLootbox(P2DbContext.Models.User currentUser, int boxType)
        {
            Random random = new Random();
            bool isShiny = false;
            Dictionary<PokemonCard, bool> result = new Dictionary<PokemonCard, bool>();

            P2DbContext.Models.PokemonCard card;
            int rareId; //generate random rarity based on preset distribution
            int rand = random.Next(101);
            if (boxType != 2) //if boxtype is not 2, we use normal odds for generating pokemon
            {
                if (rand <= 40)
                {
                    rareId = 1;
                }
                else if (rand > 40 && rand < 70)
                {
                    rareId = 2;
                }
                else if (rand >= 70 && rand < 95)
                {
                    rareId = 3;
                }
                else if (rand >= 95 && rand < 99)
                {
                    rareId = 4;
                }
                else
                {
                    rareId = 5;
                }
            }
            else //when boxtype is 2, we only generate higher teir pokemon
            {
                if (rand < 40)
                {
                    rareId = 3;
                }
                else if (rand >= 40 && rand < 90)
                {
                    rareId = 4;
                }
                else
                {
                    rareId = 5;
                }
            }
        
            //generate the random card using the rarity
            var pokeList = context.PokemonCards.Where(x => x.RarityId == rareId).ToList();  
            rand = random.Next(pokeList.Count);
            card = pokeList[rand];

            random = new Random();
            int shiny = random.Next(201);  //generate shiny odds
            if (boxType == 3) //if boxtype is 3, we guanntee a shiny
            {
                shiny = 200;
            }
            
            CardCollection collection = context.CardCollections.Where(x => x.UserId == currentUser.UserId && x.PokemonId == card.PokemonId).FirstOrDefault();
            if (collection == null)
            { //if collection is null(doesn't exist), populate the empty collection with new data and add it to the database
                collection = new CardCollection();
                collection.UserId = currentUser.UserId;
                collection.PokemonId = card.PokemonId;
                collection.QuantityNormal = 0;
                collection.QuantityShiny = 0;
                collection.IsFavorite = false;
                context.CardCollections.Add(collection);
                context.SaveChanges();
            }
            if (shiny < 199)
            { //Updates collection to reflect a new normal card
                collection.QuantityNormal++;
                context.CardCollections.Attach(collection);
                context.Entry(collection).Property(x => x.QuantityNormal).IsModified = true;
                isShiny = false;
            }
            else
            { //Updates collection to reflect a new shiny card
                collection.QuantityShiny++;
                context.CardCollections.Attach(collection);
                context.Entry(collection).Property(x => x.QuantityShiny).IsModified = true;
                isShiny = true;
            }
            currentUser.AccountLevel++;//increments account level with each lootbox opened
            context.Users.Attach(currentUser);
            context.Entry(currentUser).Property(x => x.AccountLevel).IsModified = true;
            context.SaveChanges();
            result.Add(card, isShiny);
            return result;
        }

        /// <summary>
        /// Allows user to buy a card from a post, updates database accordingly
        /// </summary>
        /// <param name="post">Post object that holds the card</param>
        /// <param name="currentUser">Current user buying</param>
        /// <returns>Dictionary object where key is the output message and value is whether or not sale was successful</returns>
        public Dictionary<string, bool> buyFromPost(Post post, User currentUser)
        {
            String output = "";
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            if (currentUser.CoinBalance < post.Price)
            { // checks if user has a sufficent balance
                output = "You don\'t have suffeiencent funds for this purchase";
                result.Add(output, false);
                return result;
            }
            if (!post.StillAvailable)
            { // checks if posts is available
                output = "Post is no longer avaialable!";
                result.Add(output, false);
                return result;
            }



            int sellerID = (int)context.DisplayBoards.Where(x => x.PostId == post.PostId).Select(x => x.UserId).FirstOrDefault();
            User seller = context.Users.Where(x => x.UserId == sellerID).FirstOrDefault();

            if (sellerID == currentUser.UserId)
            { // checks if user buys fromy themselves
                output = "You can't buy from yourself!";
                result.Add(output, false);
                return result;
            }

            currentUser.CoinBalance -= (int)post.Price; //decrement  current user coin balance
            context.Users.Attach(currentUser);
            context.Entry(currentUser).Property(x => x.CoinBalance).IsModified = true;

            seller.CoinBalance += (int)post.Price; //increment seller coin balance
            seller.TotalCoinsEarned += (int)post.Price;
            context.Users.Attach(seller);
            context.Entry(seller).Property(x => x.CoinBalance).IsModified = true;
            context.Entry(seller).Property(x => x.TotalCoinsEarned).IsModified = true;

            post.StillAvailable = false; //makes post unavailable
            context.Posts.Attach(post);
            context.Entry(post).Property(x => x.StillAvailable).IsModified = true;

            CardCollection userCollection = context.CardCollections.Where(x => x.UserId == currentUser.UserId && x.PokemonId == post.PokemonId).FirstOrDefault();
            CardCollection sellerCollection = context.CardCollections.Where(x => x.UserId == seller.UserId && x.PokemonId == post.PokemonId).FirstOrDefault();

            if (post.IsShiny != null && (bool)post.IsShiny == true)
            { //updates user and seller collection if shiny
                sellerCollection.QuantityShiny--;
                context.CardCollections.Attach(sellerCollection);
                context.Entry(sellerCollection).Property(x => x.QuantityShiny).IsModified = true;
                if (userCollection == null)
                { //if collection is null(doesn't exist), populate the empty collection with new data and add it to the database
                    userCollection = new CardCollection();
                    userCollection.UserId = currentUser.UserId;
                    userCollection.PokemonId = (int)post.PokemonId;
                    userCollection.QuantityNormal = 0;
                    userCollection.QuantityShiny = 0;
                    context.CardCollections.Add(userCollection);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        output = $"An exception occured: ${e}";
                        result.Add(output, false);
                        return result;
                    }
                }
                userCollection.QuantityShiny++;
                context.CardCollections.Attach(userCollection);
                context.Entry(userCollection).Property(x => x.QuantityShiny).IsModified = true;
                output = $"You brought a shiny {context.PokemonCards.Where(x => x.PokemonId == post.PokemonId).Select(x => x.PokemonName).FirstOrDefault()} from {seller.UserName} for {post.Price} coins!";
            }
            else
            { //updates user and seller collection if normal card
                sellerCollection.QuantityNormal--;
                context.CardCollections.Attach(sellerCollection);
                context.Entry(sellerCollection).Property(x => x.QuantityNormal).IsModified = true;
                if (userCollection == null)
                { //if collection is null(doesn't exist), populate the empty collection with new data and add it to the database
                    userCollection = new CardCollection();
                    userCollection.UserId = currentUser.UserId;
                    userCollection.PokemonId = (int)post.PokemonId;
                    userCollection.QuantityNormal = 0;
                    userCollection.QuantityShiny = 0;
                    userCollection.IsFavorite = false;
                    context.CardCollections.Add(userCollection);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        output = $"An exception occured: ${e}";
                        result.Add(output, false);
                        return result;
                    }
                }
                userCollection.QuantityNormal++;
                context.CardCollections.Attach(userCollection);
                context.Entry(userCollection).Property(x => x.QuantityNormal).IsModified = true;
                output = $"You brought a {context.PokemonCards.Where(x => x.PokemonId == post.PokemonId).Select(x => x.PokemonName).FirstOrDefault()} from {seller.UserName} for {post.Price} coins!";

            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                output = $"An exception occured: ${e}";
                result.Add(output, false);
                return result;
            }

            result.Add(output, true);
            return result;

        }

        /// <summary>
        /// List all available posts
        /// </summary>
        /// <returns>Enumable list of posts</returns>
        public List<Post> getDisplayBoard()
        {
            return context.Posts.Where(x => x.StillAvailable == true).ToList();
        }

        /// <summary>
        /// Sends a collection of pokemons cards representive if the current user collection
        /// </summary>
        /// <param name="currentUser">Current User</param>
        /// <returns>Dictionary where key is the collection object and value is pokemon card</returns>
        public Dictionary<CardCollection, PokemonCard> getUserCollection(User currentUser)
        {
            Dictionary<CardCollection, PokemonCard> result = new Dictionary<CardCollection, PokemonCard>();
            var fullCollection = context.CardCollections.Where(x => x.UserId == currentUser.UserId).ToList();
            foreach (var collection in fullCollection)
            {
                var card = context.PokemonCards.Where(x => x.PokemonId == collection.PokemonId).FirstOrDefault();
                result.Add(collection, card);
            }
            return result;
        }

        /// <summary>
        /// Inserts a new post into database
        /// </summary>
        /// <param name="newPost">post to be inserted</param>
        /// <param name="currentUser">Current user posting</param>
        /// <returns>Returns whether post has been inserted succefully</returns>
        public bool newPost(Post newPost, User currentUser)
        {

            //add new post to database after filling possible blank data         
            DateTime now = DateTime.Now;
            newPost.PostTime = now;
            newPost.StillAvailable = true;
            try
            {
                context.Posts.Add(newPost);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            int next = context.Posts.Select(x => x.PostId).Max();

            //check post details to determine post type.
            int postType = 0;
            if (newPost.PokemonId == null && newPost.Price == null)
            {
                postType = 1; //discussion
            }
            else if (newPost.Price == null)
            {
                postType = 3; //display
            }
            else
            {
                postType = 2; //sale
            }

            //reflect the new post on display board
            DisplayBoard displayBoard = new DisplayBoard();
            displayBoard.UserId = currentUser.UserId;
            displayBoard.PostType = postType;
            Console.WriteLine(context.Posts.ToList());

            displayBoard.PostId = next;//returns the newest instance of a post(the one we just added) and grabs their id.
            context.DisplayBoards.Add(displayBoard);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Allows user to log in
        /// </summary>
        /// <param name="username">Username for logging in</param>
        /// <param name="password">Password for logging in</param>
        /// <returns>User object after logging in, null if invalid creditials</returns>
        public User login(string username, string password)
        {
            return context.Users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
            //If return is null, log in is invalid and should prompt user to relogin.
        }

        /// <summary>
        /// Adds new user to database
        /// </summary>
        /// <param name="newUser">User to be added</param>
        /// <returns>true if user was successfully added to database, false otherwise</returns>
        public bool signUp(User newUser)
        {
            //set default balance and levels
            newUser.AccountLevel = 0;
            newUser.CoinBalance = 10;
            newUser.TotalCoinsEarned = 10;
            context.Add(newUser);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates user account balances to reflect a new purchase or deposit
        /// </summary>
        /// <param name="currentUser">Current user we are working on</param>
        /// <param name="coinsToAdd">Amount of coins to add to balance, value would be negetive if we are removing coins.</param>
        /// <returns>True if account succefully updated</returns>
        public bool incrementUserBalance(User currentUser, int coinsToAdd)
        {
            if (coinsToAdd >= 0)
            { //use when completing challenges(increments balance)
                currentUser.AccountLevel++; //gain levels buy completing challanges
                currentUser.CoinBalance += coinsToAdd;
                currentUser.TotalCoinsEarned += coinsToAdd;
            }
            if (coinsToAdd < 0)
            { //use when buying lootboxes(decrements balance)
                if ((coinsToAdd * -1) > currentUser.CoinBalance)
                {//do you have correct amount of coins to buy?
                    return false;
                }
                currentUser.CoinBalance += coinsToAdd;
            }
            context.Users.Attach(currentUser);
            context.Entry(currentUser).Property(x => x.CoinBalance).IsModified = true;
            if (coinsToAdd >= 0)
            {
                context.Entry(currentUser).Property(x => x.TotalCoinsEarned).IsModified = true;
                context.Entry(currentUser).Property(x => x.AccountLevel).IsModified = true;
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves pokemon using id for display purposes
        /// </summary>
        /// <param name="id">Pokemon Id</param>
        /// <returns>Pokemon card with giving id, retuns null if invalid</returns>
        public PokemonCard getPokemonById(int id)
        {
            return context.PokemonCards.Where(x => x.PokemonId == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user object by its Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User object or null</returns>
        public User GetUserById(int id)
        {
            return context.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

        /// <summary>
        /// Removes user object by its Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>True if user was deleted successfully or false otherwise</returns>
        public bool RemoveUser(int id)
        {
            // Grab the Object by id
            User user = context.Users.Where(x => x.UserId == id).FirstOrDefault();

            // Remove the user.
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }

            return false;
        }
        /// Gets additional information on the post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>DisplayBoard object or null</returns>
        public DisplayBoard getPostInfo(int id)
        {
            return context.DisplayBoards.Where(x => x.PostId == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets post object
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>Post object or null</returns>
        public Post getPostById(int id)
        {
            return context.Posts.Where(x => x.PostId == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets avaialble rarities
        /// </summary>
        /// <returns>List of rarity types</returns>
        public List<RarityType> GetRarityTypes()
        {
            return context.RarityTypes.ToList();
        }

        /// <summary>
        /// Hides post from display boarrd
        /// </summary>
        /// <param name="PostID">Post Id of post you want to remive</param>
        /// <returns>True if succefully removed, false</returns>
        public bool hidePost(int PostID)
        {
            Post post = context.Posts.Where(x => x.PostId == PostID).FirstOrDefault();
            if (post == null)
            {
                return false;
            }
            //by setting is available to false, we can remove a post from display board without removing any data from our database, keeping statistics and depenies intact
            //great for testing
            post.StillAvailable = false; 
            context.Posts.Attach(post);
            context.Entry(post).Property(x => x.StillAvailable).IsModified = true;
            try
            {
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Edits current price of a sales post
        /// </summary>
        /// <param name="postID">Post Id of the post to edit</param>
        /// <param name="newPrice">Price you want to change to</param>
        /// <returns>True if succesfully edited, false otherwise</returns>
        public bool editPrice(int postID, int newPrice)
        {
            Post post = context.Posts.Where(x => x.PostId == postID).FirstOrDefault();
            if (post == null)
            {
                return false;
            }
            if(post.Price == null) //check if post is a sale
            {
                return false;
            }
            post.Price = newPrice;
            context.Posts.Attach(post);
            context.Entry(post).Property(x => x.Price).IsModified = true;
            try
            {
                context.SaveChanges();
            }
            catch
            {
                return false;
            }



            return true;
        }

        public bool newPostComment(int userId, int postId, string content)
        {      
            PostComment newPostComment = new PostComment();

            newPostComment.CommentUserId    = userId;
            newPostComment.CommentPostId    = postId;
            newPostComment.CommentContent   = content;
            newPostComment.CommentTimestamp = DateTime.Now;

            try
            {
                context.PostComments.Add(newPostComment);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Gets a list of all comment objects related to the requested post id
        /// </summary>
        /// <param name="postId">The id of the post to retrieve the comments for</param>
        /// <returns>List of comment objects for specified post</returns>
        public List<PostComment> getCommentList(int postId)
        {
            List<PostComment> commentList = context.PostComments.Where(x => x.CommentPostId == postId).ToList();
            return commentList;
        }


        /// <summary>
        /// Switches the favorite status of a specific card, effects both shiny and non shiny cards
        /// </summary>
        /// <param name="UserId">Current User ID</param>
        /// <param name="Poke">Pokemon Id of card you want to switch</param>
        /// <returns>True if favorite status if succesfully flipped, false otherwise</returns>
        public bool favoriteCard(int UserId, int Poke)
        {
            CardCollection card = context.CardCollections.Where(x => x.UserId == UserId && x.PokemonId == Poke).FirstOrDefault();
            if(card == null)
            {
                return false;
            }
            if(card.IsFavorite == null) //changes all null instances to false which will be switched after
            {
                card.IsFavorite = false;
            }
            card.IsFavorite = !card.IsFavorite;
            context.CardCollections.Attach(card);
            context.Entry(card).Property(x => x.IsFavorite).IsModified = true;
            try
            {
                context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Gets all current friends and unaccepted frint requests
        /// </summary>
        /// <param name="UserId">Current User ID</param>
        /// <returns>List of fullfriend objects</returns>
        public List<FullFriend> GetFriends(int UserId)
        {
            List<FullFriend> fullFriends = new List<FullFriend>();
            var friends = context.FriendsLists.Where(x => x.SentRequest == UserId || x.RecievedRequest == UserId).ToList(); //recieves all cases where user id appears in friend list
            //return an empty list if result is null or empty
            if(friends == null)
            {
                return fullFriends;
            }
            if (!friends.Any())
            {
                return fullFriends;
            }
            foreach(FriendsList friend in friends)
            {
                
                if(friend.SentRequest == UserId)
                {
                    if(friend.IsPending == false) //where user sent a request and is no longer pending, this is considered a friend and will appear in the list
                    {
                        User friendInfo = context.Users.Where(x => x.UserId == friend.RecievedRequest).FirstOrDefault();
                        int totalCards = context.CardCollections.Where(x => x.UserId == friend.RecievedRequest).ToList().Count();
                        FullFriend fullFriend = new FullFriend() {
                            FriendName = friendInfo.UserName,
                            FriendLevel = friendInfo.AccountLevel,
                            TotalCards = totalCards,
                            IsPending = false,
                            DateAdded = friend.DateAdded,
                            FriendId = friend.RecievedRequest
                        };
                        fullFriends.Add(fullFriend);
                    }
                }
                if (friend.RecievedRequest == UserId) // if user recieved a request, the instance will appear in the list in all cases
                {                  
                        User friendInfo = context.Users.Where(x => x.UserId == friend.SentRequest).FirstOrDefault();
                        int totalCards = context.CardCollections.Where(x => x.UserId == friend.SentRequest).ToList().Count();
                        FullFriend fullFriend = new FullFriend()
                        {
                            FriendName = friendInfo.UserName,
                            FriendLevel = friendInfo.AccountLevel,
                            TotalCards = totalCards,
                            IsPending = friend.IsPending, //if is pending is still true, user will have option to filter out pending request and/or accept them
                            DateAdded = friend.DateAdded,
                            FriendId = friend.SentRequest
                        };
                        fullFriends.Add(fullFriend);                    
                }
            }
            return fullFriends;
        }


        /// <summary>
        /// Does various actions based on friendship status between two users
        /// </summary>
        /// <param name="userid">Current User Id</param>
        /// <param name="friendId">Intended friend id</param>
        /// <returns>Outputs a string with information on friendship status and if anything has changed</returns>
        public string friendAction(int userid, int friendId)
        {
            if(userid == friendId) //Stops user from befriending themselves
            {
                return "You can't be friends with yourself!";
            }
            //Grabs case where friend id or user id may appear in friend list
            //due to checks later on, we prevent any cases where the duo can appear twice in the db
            FriendsList friends = context.FriendsLists.Where(x => (x.SentRequest == userid && x.RecievedRequest == friendId) || (x.RecievedRequest == userid && x.SentRequest == friendId)).FirstOrDefault();
            string friendName = context.Users.Where(x => x.UserId == friendId).Select(x => x.UserName).FirstOrDefault();

            if(friends == null)//friend object doesnt exist, therefore we create a new instance where current user sends the request and awaits acceptance
            {
                friends = new FriendsList()
                {
                    SentRequest = userid,
                    RecievedRequest = friendId,
                    IsPending = true,
                    DateAdded = DateTime.Now
                };
                context.FriendsLists.Add(friends);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return $"Expeption {e} occurred while sending this request.";
                }
                return $"You sent a request to {friendName}!";

            }

            if(friends.IsPending == false) //if pending is false, then they are already friends
            {
                return $"You are already friends with {friendName}!";
            }

            //anything after this point, is pending is true, so one party is awaiting a request.

            if(friends.RecievedRequest == userid)//if user recieved the request, we accept the pending request and updates the instance 
            {
                friends.IsPending = false;
                context.FriendsLists.Attach(friends);
                context.Entry(friends).Property(x => x.IsPending).IsModified = true;
                try
                {
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    return $"Expeption {e} occurred while accepting this request.";
                }
                return $"Your are now friends with {friendName}!";
            }
            if(friends.SentRequest == userid)//if user sent the request, we do nothing and notify the user the request is still pending.
            {
                return $"You already sent a request to {friendName}, wait for them to accept it.";
            }


            return "Oops, we hit a weird edge case...";
        }
        


    }//class BusinessModel
}// namespace BusinessLayer
