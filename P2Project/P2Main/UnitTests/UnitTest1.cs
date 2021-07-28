using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P2DbContext.Models;
using BusinessLayer;
using System.Collections.Generic;

namespace UnitTests
{
    public class UnitTest1
    {

        DbContextOptions<P2DbClass> options = new DbContextOptionsBuilder<P2DbClass>().UseInMemoryDatabase(databaseName: "TestingDb").Options;

        [Fact]
        public void signUpTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password"

            };
            bool result = false;
            User resultUser;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                result = testBusinessModel.signUp(testUser);
                resultUser = context.Users.FirstOrDefault();

                // Assert
                Assert.True(result);
                Assert.True(resultUser != null);
                Assert.True(context.Users.ToList().Any());
                Assert.True(resultUser.UserName == testUser.UserName);
                Assert.True(resultUser.UserId > 0);
                Assert.True(resultUser.CoinBalance == 10);


            }
        }

        [Fact]
        public void loginTest()
        {
            // Arange

            string testUserName = "genericUser";
            string testPassword = "Password";
            string failUserName = "failedUser";
            string failPassword = "failedUser";
            string testCaseUserName = "genericuser";
            string testCasePassword = "password";

            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };
            User resultUser;
            User failUser;
            User caseUser;
            User unmatchedUser;
            User unmatchedPassword;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.SaveChanges();

                resultUser = testBusinessModel.login(testUserName, testPassword);
                failUser = testBusinessModel.login(failUserName, failPassword);
                caseUser = testBusinessModel.login(testCaseUserName, testCasePassword);
                unmatchedUser = testBusinessModel.login(failUserName, testPassword);
                unmatchedPassword = testBusinessModel.login(testUserName, failPassword);

                // Assert
                Assert.True(resultUser != null);
                Assert.True(failUser == null);
                Assert.True(caseUser == null);
                Assert.True(unmatchedUser == null);
                Assert.True(unmatchedPassword == null);
                Assert.True(resultUser.UserName == testUser.UserName);
                Assert.True(resultUser.Password == testUser.Password);



            }
        }
        [Fact]
        public void getUserByIdTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };
            User resultUser;
            User failUser;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.SaveChanges();

                resultUser = testBusinessModel.GetUserById(1);
                failUser = testBusinessModel.GetUserById(2);

                // Assert
                Assert.True(resultUser != null);
                Assert.True(failUser == null);
                Assert.True(resultUser.UserName == testUser.UserName);
                Assert.True(resultUser.Password == testUser.Password);

            }
        }

        [Fact]
        public void getPokemonByIdTest()
        {
            // Arange
            PokemonCard testPokemon = new PokemonCard()
            {
                PokemonId = 150,
                PokemonName = "mewtwo",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/150.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/150.png",
            };

            PokemonCard resultPokemon;
            PokemonCard failPokemon;
            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.PokemonCards.Add(testPokemon);
                context.SaveChanges();

                resultPokemon = testBusinessModel.getPokemonById(150);
                failPokemon = testBusinessModel.getPokemonById(29899778);

                // Assert
                Assert.True(resultPokemon != null);
                Assert.True(failPokemon == null);
                Assert.True(resultPokemon.PokemonId == testPokemon.PokemonId);
                Assert.True(resultPokemon.PokemonName == testPokemon.PokemonName);

            }
        }

        [Fact]
        public void incrementUserBalanceTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };

            User failUser = new User { UserId = 2 };
            User resultUser;
            bool resultUserInc;
            bool resultUserDec;
            bool failResultUserInc;
            bool failResultUserDec;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.SaveChanges();
                testUser.UserId = 1;

                resultUserInc = testBusinessModel.incrementUserBalance(testUser, 10);
                resultUserDec = testBusinessModel.incrementUserBalance(testUser, -10);
                failResultUserInc = testBusinessModel.incrementUserBalance(failUser, 10);
                failResultUserDec = testBusinessModel.incrementUserBalance(testUser, -30);
                resultUser = context.Users.Where(x => x.UserId == testUser.UserId).FirstOrDefault();

                // Assert
                Assert.True(resultUserInc);
                Assert.True(resultUserDec);
                Assert.True(!failResultUserInc);
                Assert.True(!failResultUserDec);
                Assert.True(resultUser.AccountLevel == 1);
                Assert.True(resultUser.CoinBalance == 10);
                Assert.True(resultUser.TotalCoinsEarned == 20);


            }
        }

        [Fact]
        public void getDisplayBoardTest()
        {
            // Arange
            Post testPost1 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post",
                Price = 20,
                StillAvailable = true,
            };
            Post testPost2 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post with a shiny",
                Price = 20,
                StillAvailable = true,
                IsShiny = true,
            };
            Post testPost3 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post with a shiny thats not avaialable",
                Price = 20,
                StillAvailable = false,
                IsShiny = true,
            };
            Post testPost4 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a display post",
                StillAvailable = true,
            };
            Post testPost5 = new Post()
            {
                PostTime = DateTime.Now,
                PostDescription = "this is a discussion  post",
                StillAvailable = true,
            };
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };
            DisplayBoard displayBoard1 = new DisplayBoard()
            {
                UserId = 1,
                PostId = 1,
                PostType = 2,
            };
            DisplayBoard displayBoard2 = new DisplayBoard()
            {
                UserId = 1,
                PostId = 2,
                PostType = 2,
            };
            DisplayBoard displayBoard3 = new DisplayBoard()
            {
                UserId = 1,
                PostId = 3,
                PostType = 2,
            };
            DisplayBoard displayBoard4 = new DisplayBoard()
            {
                UserId = 1,
                PostId = 4,
                PostType = 3,
            };
            DisplayBoard displayBoard5 = new DisplayBoard()
            {
                UserId = 1,
                PostId = 5,
                PostType = 1,
            };
            List<Post> testPosts;



            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.Posts.Add(testPost1);
                context.Posts.Add(testPost2);
                context.Posts.Add(testPost3);
                context.Posts.Add(testPost4);
                context.Posts.Add(testPost5);
                context.DisplayBoards.Add(displayBoard1);
                context.DisplayBoards.Add(displayBoard2);
                context.DisplayBoards.Add(displayBoard3);
                context.DisplayBoards.Add(displayBoard4);
                context.DisplayBoards.Add(displayBoard5);
                context.SaveChanges();
                testUser.UserId = 1;

                testPosts = testBusinessModel.getDisplayBoard();

                // Assert
                Assert.True(testPosts.Any());
                Assert.True(testPosts.Count == 4);


            }
        }

        [Fact]
        public void newPostTest()
        {
            // Arange

            Post testPost1 = new Post()
            {
                PokemonId = 150,
                PostDescription = "this is a sales post",
                Price = 20,
            };
            Post testPost2 = new Post()
            {
                PokemonId = 150,
                PostDescription = "this is a display post",
            };
            Post testPost3 = new Post()
            {
                PostDescription = "this is a discussion  post",
            };
            Post testPost4 = new Post()
            {
                PostId = 1,
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post",
                Price = 20,
                StillAvailable = true,
            };
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };

            bool post1;
            bool post2;
            bool post3;
            bool post4;
            DisplayBoard resultPost1;
            DisplayBoard resultPost2;
            DisplayBoard resultPost3;


            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.SaveChanges();
                testUser.UserId = 1;


                post1 = testBusinessModel.newPost(testPost1, testUser);
                post2 = testBusinessModel.newPost(testPost2, testUser);
                post3 = testBusinessModel.newPost(testPost3, testUser);
                post4 = testBusinessModel.newPost(testPost4, testUser);
                resultPost1 = context.DisplayBoards.Where(x => x.PostId == 1).FirstOrDefault();
                resultPost2 = context.DisplayBoards.Where(x => x.PostId == 2).FirstOrDefault();
                resultPost3 = context.DisplayBoards.Where(x => x.PostId == 3).FirstOrDefault();

                // Assert
                Assert.True(post1);
                Assert.True(post2);
                Assert.True(post3);
                Assert.True(!post4);
                Assert.Equal(2, resultPost1.PostType);
                Assert.Equal(3, resultPost2.PostType);
                Assert.Equal(1, resultPost3.PostType);


            }
        }
        [Fact]
        public void getUserCollectionTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };
            User testUser2 = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };
            PokemonCard testPokemon1 = new PokemonCard()
            {
                PokemonId = 150,
                PokemonName = "mewtwo",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/150.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/150.png",
            };
            PokemonCard testPokemon2 = new PokemonCard()
            {
                PokemonId = 151,
                PokemonName = "mew",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/151.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/151.png",
            };
            CardCollection cardCollection1 = new CardCollection()
            {
                PokemonId = 150,
                UserId = 1,
                QuantityNormal = 1,
                QuantityShiny = 0
            };
            CardCollection cardCollection2 = new CardCollection()
            {
                PokemonId = 151,
                UserId = 1,
                QuantityNormal = 0,
                QuantityShiny = 1
            };
            

            Dictionary<CardCollection, PokemonCard> result;
            Dictionary<CardCollection, PokemonCard> failResult;


            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.Users.Add(testUser2);
                context.PokemonCards.Add(testPokemon1);
                context.PokemonCards.Add(testPokemon2);
                context.CardCollections.Add(cardCollection1);
                context.CardCollections.Add(cardCollection2);
                context.SaveChanges();
                testUser.UserId = 1;
                testUser2.UserId = 2;

                result = testBusinessModel.getUserCollection(testUser);
                failResult = testBusinessModel.getUserCollection(testUser2);


                // Assert
                Assert.True(result.Any());
                Assert.True(failResult.Keys.Count == 0);
                Assert.True(result.Keys.Count == 2);
                Assert.True(result.Keys.ToArray()[0].UserId == 1);
                Assert.True(result.Keys.ToArray()[0].PokemonId == 150);
                Assert.True(result.Keys.ToArray()[1].QuantityNormal == 0);
                Assert.True(result.Keys.ToArray()[1].QuantityShiny == 1);
                Assert.True(result.Values.Any());
            }
        }

        [Fact]
        public void rollLootboxTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };

            PokemonCard testPokemon1 = new PokemonCard()
            {
                RarityId = 1,
                PokemonId = 150,
                PokemonName = "mewtwo",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/150.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/150.png",
            };
            PokemonCard testPokemon2 = new PokemonCard()
            {

                RarityId = 2,
                PokemonId = 151,
                PokemonName = "mew",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/151.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/151.png",
            };
            PokemonCard testPokemon3 = new PokemonCard()
            {
                RarityId = 3,
                PokemonId = 152,
                PokemonName = "chikorita",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/152.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/152.png",
            };
            PokemonCard testPokemon4 = new PokemonCard()
            {
                RarityId = 4,
                PokemonId = 153,
                PokemonName = "bayleef",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/153.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/153.png",
            };
            PokemonCard testPokemon5 = new PokemonCard()
            {
                RarityId = 5,
                PokemonId = 154,
                PokemonName = "meganium",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/154.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/154.png",
            };
            CardCollection cardCollection1 = new CardCollection()
            {
                PokemonId = 150,
                UserId = 1,
                QuantityNormal = 1,
                QuantityShiny = 0
            };


            Dictionary<PokemonCard, bool> result1;
            Dictionary<PokemonCard, bool> result2;
            Dictionary<PokemonCard, bool> result3;
            Dictionary<PokemonCard, bool> result4;
            Dictionary<PokemonCard, bool> result5;
            Dictionary<PokemonCard, bool> result6;
            Dictionary<PokemonCard, bool> result7;
            Dictionary<PokemonCard, bool> result8;
            Dictionary<PokemonCard, bool> result9;
            Dictionary<PokemonCard, bool> result10;
            Dictionary<PokemonCard, bool> result11;
            List<CardCollection> cardCollection;
            User resultUser;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.PokemonCards.Add(testPokemon1);
                context.PokemonCards.Add(testPokemon2);
                context.PokemonCards.Add(testPokemon3);
                context.PokemonCards.Add(testPokemon4);
                context.PokemonCards.Add(testPokemon5);
                context.CardCollections.Add(cardCollection1);
                context.SaveChanges();
                testUser.UserId = 1;

                //since this uses rng, its impossible to test all the edge cases, but ran this about 10 times and had no errors
                result1 = testBusinessModel.rollLootbox(testUser);
                result2 = testBusinessModel.rollLootbox(testUser);
                result3 = testBusinessModel.rollLootbox(testUser);
                result4 = testBusinessModel.rollLootbox(testUser);
                result5 = testBusinessModel.rollLootbox(testUser);
                result6 = testBusinessModel.rollLootbox(testUser);
                result7 = testBusinessModel.rollLootbox(testUser);
                result8 = testBusinessModel.rollLootbox(testUser);
                result9 = testBusinessModel.rollLootbox(testUser);
                result10 = testBusinessModel.rollLootbox(testUser);
                result11 = testBusinessModel.rollLootbox(testUser);
                cardCollection = context.CardCollections.Where(x => x.UserId == testUser.UserId).ToList();
                resultUser = context.Users.Where(x => x.UserId == testUser.UserId).FirstOrDefault();


                // Assert
                Assert.True(resultUser.AccountLevel == 11);
                Assert.True(result1.Any());
                Assert.True(result1.Keys.Count == 1);
                Assert.True(result1.Values.Any());
                Assert.True(result1.Values.Count == 1);
                Assert.True(result2.Any());
                Assert.True(result2.Keys.Count == 1);
                Assert.True(result2.Values.Any());
                Assert.True(result1.Values.Count == 1);
                Assert.True(result3.Any());
                Assert.True(result3.Keys.Count == 1);
                Assert.True(result3.Values.Any());
                Assert.True(result3.Values.Count == 1);
                Assert.True(result4.Any());
                Assert.True(result4.Keys.Count == 1);
                Assert.True(result4.Values.Any());
                Assert.True(result4.Values.Count == 1);
                Assert.True(result5.Any());
                Assert.True(result5.Keys.Count == 1);
                Assert.True(result5.Values.Any());

            }
        }

        [Fact]
        public void buyFromPostTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 30,
                TotalCoinsEarned = 10,

            };
            User testSeller = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic2@email.com",
                UserName = "genericSeller",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 20,
                TotalCoinsEarned = 30,

            };
            Post testPost1 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post",
                Price = 20,
                StillAvailable = true,
            };
            Post testPost2 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post with a shiny",
                Price = 20,
                StillAvailable = true,
                IsShiny = true,
            };
            Post testPost3 = new Post()
            {
                PokemonId = 151,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post with a shiny",
                Price = 5,
                StillAvailable = true,
                IsShiny = true,
            };
            Post testPost4 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post with a shiny thats not avaialable",
                Price = 20,
                StillAvailable = false,
                IsShiny = true,
            };
            PokemonCard testPokemon1 = new PokemonCard()
            {
                PokemonId = 150,
                PokemonName = "mewtwo",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/150.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/150.png",
            };
            PokemonCard testPokemon2 = new PokemonCard()
            {
                PokemonId = 151,
                PokemonName = "mew",
                SpriteLink = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/151.png",
                SpriteLinkShiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/151.png",
            };
            CardCollection cardCollection1 = new CardCollection()
            {
                PokemonId = 150,
                UserId = 2,
                QuantityNormal = 2,
                QuantityShiny = 2
            };
            CardCollection cardCollection2 = new CardCollection()
            {
                PokemonId = 151,
                UserId = 2,
                QuantityNormal = 2,
                QuantityShiny = 2
            };
            CardCollection cardCollection3 = new CardCollection()
            {
                PokemonId = 150,
                UserId = 1,
                QuantityNormal = 2,
                QuantityShiny = 2
            };
            DisplayBoard displayBoard1 = new DisplayBoard()
            {
                UserId = 2,
                PostId = 1,
                PostType = 2,
            };
            DisplayBoard displayBoard2 = new DisplayBoard()
            {
                UserId = 2,
                PostId = 2,
                PostType = 2,
            };
            DisplayBoard displayBoard3 = new DisplayBoard()
            {
                UserId = 2,
                PostId = 3,
                PostType = 2,
            };
            DisplayBoard displayBoard4 = new DisplayBoard()
            {
                UserId = 2,
                PostId = 4,
                PostType = 2,
            };
            Dictionary<string, bool> failUnavailable;
            Dictionary<string, bool> resultTrue;
            Dictionary<string, bool> falseBalance;
            Dictionary<string, bool> falseSame;
            Dictionary<string, bool> trueNewShiny;
            User resultSeller;
            User resultUser;
            CardCollection sellerMewtwo;
            CardCollection sellerMew;
            CardCollection userMewtwo;
            CardCollection userMew;
            Post resultPost;
            Post failPost;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.Users.Add(testSeller);
                context.PokemonCards.Add(testPokemon1);
                context.PokemonCards.Add(testPokemon2);
                context.Posts.Add(testPost1);
                context.Posts.Add(testPost2);
                context.Posts.Add(testPost3);
                context.Posts.Add(testPost4);
                context.DisplayBoards.Add(displayBoard1);
                context.DisplayBoards.Add(displayBoard2);
                context.DisplayBoards.Add(displayBoard3);
                context.DisplayBoards.Add(displayBoard4);
                context.CardCollections.Add(cardCollection1);
                context.CardCollections.Add(cardCollection2);
                context.CardCollections.Add(cardCollection3);
                context.SaveChanges();
                testUser.UserId = 1;
                testSeller.UserId = 2;
                testPost1.PostId = 1;
                testPost2.PostId = 2;
                testPost3.PostId = 3;
                testPost4.PostId = 4;

                falseSame = testBusinessModel.buyFromPost(testPost1, testSeller);
                failUnavailable = testBusinessModel.buyFromPost(testPost4, testUser); //false unavailble
                resultTrue = testBusinessModel.buyFromPost(testPost1, testUser); //true
                falseBalance = testBusinessModel.buyFromPost(testPost2, testUser); //false no balance
                trueNewShiny = testBusinessModel.buyFromPost(testPost3, testUser); //true with shiny and new item
                resultUser = context.Users.Where(x => x.UserId == testUser.UserId).FirstOrDefault();
                resultSeller = context.Users.Where(x => x.UserId == testSeller.UserId).FirstOrDefault();
                sellerMewtwo = context.CardCollections.Where(x => x.UserId == testSeller.UserId && x.PokemonId == 150).FirstOrDefault();
                sellerMew = context.CardCollections.Where(x => x.UserId == testSeller.UserId && x.PokemonId == 151).FirstOrDefault();
                userMewtwo = context.CardCollections.Where(x => x.UserId == testUser.UserId && x.PokemonId == 150).FirstOrDefault();
                userMew = context.CardCollections.Where(x => x.UserId == testUser.UserId && x.PokemonId == 151).FirstOrDefault();
                resultPost = context.Posts.Where(x => x.PostId == 1).FirstOrDefault();
                failPost = context.Posts.Where(x => x.PostId == 2).FirstOrDefault();

                // Assert
                Assert.True(!falseSame.Values.ToArray()[0]);
                Assert.True(!resultPost.StillAvailable);
                Assert.True(failPost.StillAvailable);
                Assert.True(resultTrue.Values.ToArray()[0]);
                Assert.True(!falseBalance.Values.ToArray()[0]);
                Assert.True(trueNewShiny.Values.ToArray()[0]);
                Assert.True(!failUnavailable.Values.ToArray()[0]);
                Assert.Equal(5, resultUser.CoinBalance);
                Assert.Equal(45, resultSeller.CoinBalance);
                Assert.Equal(55, resultSeller.TotalCoinsEarned);
                Assert.Equal(2, sellerMew.QuantityNormal);
                Assert.Equal(1, sellerMew.QuantityShiny);
                Assert.Equal(1, sellerMewtwo.QuantityNormal);
                Assert.Equal(2, sellerMewtwo.QuantityShiny);
                Assert.Equal(0, userMew.QuantityNormal);
                Assert.Equal(1, userMew.QuantityShiny);
                Assert.Equal(3, userMewtwo.QuantityNormal);
                Assert.Equal(2, userMewtwo.QuantityShiny);


            }
        }

        [Fact]
        public void deleteUserTest()
        {
            // Arange
            User testUser = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = "generic@email.com",
                UserName = "genericUser",
                Password = "Password",
                AccountLevel = 0,
                CoinBalance = 10,
                TotalCoinsEarned = 10,

            };
            bool resultPass;
            User resultUser;
            bool resultFail;

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);

                context.Users.Add(testUser);
                context.SaveChanges();

                resultPass = testBusinessModel.RemoveUser(1);
                resultFail = testBusinessModel.RemoveUser(2);
                resultUser = context.Users.Where(x => x.UserId == 1).FirstOrDefault();

                // Assert
                Assert.True(resultPass);
                Assert.True(resultUser == null);
                Assert.True(!resultFail);

            }
        }

        [Fact]
        public void getPostbyIdTest()
        {
            // Arange
            Post testPost1 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post",
                Price = 20,
                StillAvailable = true,
            };

            Post failPost;
            Post resultPost;
            Post realPost;
            

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);


                context.Posts.Add(testPost1);
                context.SaveChanges();

                resultPost = testBusinessModel.getPostById(1);
                failPost = testBusinessModel.getPostById(2);
                realPost = context.Posts.Where(x => x.PostId == 1).FirstOrDefault();

                // Assert
                Assert.True(resultPost != null);
                Assert.True(failPost == null);
                Assert.Equal(resultPost.PostId, realPost.PostId);
                Assert.Equal(resultPost.PostTime, realPost.PostTime);
                Assert.Equal(resultPost.PostDescription, realPost.PostDescription);

            }
        }
        [Fact]
        public void buildWithContext()
        {
            // Arange
            Post testPost1 = new Post()
            {
                PokemonId = 150,
                PostTime = DateTime.Now,
                PostDescription = "this is a sales post",
                Price = 20,
                StillAvailable = true,
            };

            Post realPost;
    

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);


                context.Posts.Add(testPost1);
                context.SaveChanges();

              
                realPost = context.Posts.Where(x => x.PostId == 1).FirstOrDefault();

                // Assert
                Assert.Equal(context, testBusinessModel.context);
                Assert.Equal(1, realPost.PostId);
                Assert.Equal("this is a sales post", realPost.PostDescription);

            }
        }

        [Fact]
        public void buildNoContext()
        {
            // Arange
           

            User realUser;


            // Act
                      

                BusinessModel testBusinessModel = new BusinessModel();



                realUser = testBusinessModel.context.Users.Where(x => x.UserId == 1).FirstOrDefault();

                // Assert
                Assert.True(testBusinessModel.context != null);
                Assert.Equal(1, realUser.UserId);
                Assert.Equal("alain.duplan", realUser.UserName);

            
        }

        [Fact]
        public void getRarityTypesTest()
        {
            // Arange
            RarityType testRarity1 = new RarityType()
            {
                RarityId = 1,
                RarityCategory = "common"
            };
            RarityType testRarity2 = new RarityType()
            {
                RarityId = 2,
                RarityCategory = "uncommon"
            };

            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);


                context.RarityTypes.Add(testRarity1);
                context.RarityTypes.Add(testRarity2);
                context.SaveChanges();

                List<RarityType> resultList = testBusinessModel.GetRarityTypes();

                // Assert
                Assert.Equal(2, resultList.Count);

            }
        }

        [Fact]
        public void getPostInfoTest()
        {
            // Arange
            DisplayBoard displayBoard1 = new DisplayBoard()
            {
                UserId = 1,
                PostId = 1,
                PostType = 2,
            };

            DisplayBoard failPost;
            DisplayBoard resultPost;
            DisplayBoard realPost;


            // Act
            using (var context = new P2DbClass(options))    // creates in memory database
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                BusinessModel testBusinessModel = new BusinessModel(context);


                context.DisplayBoards.Add(displayBoard1);
                context.SaveChanges();

                resultPost = testBusinessModel.getPostInfo(1);
                failPost = testBusinessModel.getPostInfo(2);
                realPost = context.DisplayBoards.Where(x => x.PostId == 1).FirstOrDefault();

                // Assert
                Assert.True(resultPost != null);
                Assert.True(failPost == null);
                Assert.Equal(resultPost.PostId, realPost.PostId);
                Assert.Equal(resultPost.UserId, realPost.UserId);
                Assert.Equal(resultPost.PostType, realPost.PostType);

            }
        }


    }

        
    }


