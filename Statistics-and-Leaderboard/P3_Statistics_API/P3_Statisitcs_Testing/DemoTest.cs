using Microsoft.EntityFrameworkCore;
using RepositoryModels;
using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using BuisinessLayerMethods;
using System.Collections.Generic;
using System.Data.Common;
using System.Configuration;

namespace P3_Statisitcs_Testing
{
    public class DemoTest
    {
        DbContextOptions<P3Context> options = new DbContextOptionsBuilder<P3Context>().UseInMemoryDatabase(databaseName: "testingDb").Options;


        //================================List Top Coins Balance============================
        
        [Fact]
        public void TopCurrentBallanceTest()
        {
            //Arrange



            // Create a dummy user 
            User user1 = new User()
            {
                UserId = 1,
                CoinBalance = 5,
                TotalCoinsEarned = 10
            };

            // Create a dummy user 
            User user2 = new User()
            {
                UserId = 2,
                CoinBalance = 2,
                TotalCoinsEarned = 100
            };

            // Create a dummy user 
            User user3 = new User()
            {
                UserId = 3,
                CoinBalance = 7,
                TotalCoinsEarned = 200
            };



            //Act
            using (var context = new P3Context(options))
            {
                //verify that the db was deleted and created anew
                context.Database.EnsureDeleted();//delete any Db from a previous test

                context.Database.EnsureCreated();// create a new Db.. you will need to seed it again.

                LeaderboardModel usertest = new LeaderboardModel(context);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);


                context.SaveChanges();
                //Assert

                List<UserCoinBalance> list1 = usertest.TopCurrentBallance(2);

                Assert.NotNull(list1);
                Assert.Equal(2, list1.Count());
            }
        }


        //================================List Top Earned Coins ============================
        [Fact]
        public void TopEarnedCoinsTest()
        {
            //Arrange



            // Create a dummy user 
            User user1 = new User()
            {
                UserId = 1,
                CoinBalance = 5,
                TotalCoinsEarned = 10
            };

            // Create a dummy user 
            User user2 = new User()
            {
                UserId = 2,
                CoinBalance = 2,
                TotalCoinsEarned = 100
            };

            // Create a dummy user 
            User user3 = new User()
            {
                UserId = 3,
                CoinBalance = 7,
                TotalCoinsEarned = 200
            };



            //Act
            using (var context = new P3Context(options))
            {
                //verify that the db was deleted and created anew
                context.Database.EnsureDeleted();//delete any Db from a previous test

                context.Database.EnsureCreated();// create a new Db.. you will need to seed it again.

                LeaderboardModel usertest = new LeaderboardModel(context);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);


                context.SaveChanges();
                //Assert

                List<UserCoinEarned> list1 = usertest.TopEarnedCoins(2);

                Assert.NotNull(list1);
                Assert.Equal(2, list1.Count());
            }
        }



        //================================List Top Spent Coins ============================
        [Fact]
        public void TopSpentCoinsTest()
        {
            //Arrange



            // Create a dummy user 
            User user1 = new User()
            {
                UserId = 1,
                CoinBalance = 5,
                TotalCoinsEarned = 10
            };

            // Create a dummy user 
            User user2 = new User()
            {
                UserId = 2,
                CoinBalance = 2,
                TotalCoinsEarned = 100
            };

            // Create a dummy user 
            User user3 = new User()
            {
                UserId = 3,
                CoinBalance = 7,
                TotalCoinsEarned = 200
            };



            //Act
            using (var context = new P3Context(options))
            {
                //verify that the db was deleted and created anew
                context.Database.EnsureDeleted();//delete any Db from a previous test

                context.Database.EnsureCreated();// create a new Db.. you will need to seed it again.

                LeaderboardModel usertest = new LeaderboardModel(context);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);


                context.SaveChanges();
                //Assert

                List<UserCoinSpent> list1 = usertest.TopSpentCoins(2);

                Assert.NotNull(list1);
                Assert.Equal(2, list1.Count());
            }
        }




        //================================List Top Percentage of collected cards ============================
        [Fact]
        public void TopPercentageCompletedCollectionTest()
        {
            //Arrange
            // ConnectionStringSettings dbconext= ConfigurationManager.ConnectionStrings["Server=localhost\\SQLEXPRESS;Database=p3;Trusted_Connection=True;"];
            // DbConnection conn;
            // DbConnection context = dbconext.ConnectionString;
            // string conn = ;


            // P3Context context = new P3Context();
            // DbConnection conn = context.Database.OpenConnection("Server=localhost\\SQLEXPRESS;Database=p3;Trusted_Connection=True;");





            // Create a dummy user 
            User user1 = new User()
            {
                UserId = 1,
                CoinBalance = 5,
                TotalCoinsEarned = 10
            };

            // Create a dummy user 
            User user2 = new User()
            {
                UserId = 2,
                CoinBalance = 2,
                TotalCoinsEarned = 100
            };

            // Create a dummy user 
            User user3 = new User()
            {
                UserId = 3,
                CoinBalance = 7,
                TotalCoinsEarned = 200
            };


            CardCollection collection1 = new CardCollection()
            {
                PokemonId = 4,
                UserId = 2,
                QuantityNormal = 14,
                QuantityShiny = 13
            };

            CardCollection collection2 = new CardCollection()
            {
                PokemonId = 2,
                UserId = 3,
                QuantityNormal = 14,
                QuantityShiny = 13
            };
            CardCollection collection3 = new CardCollection()
            {
                PokemonId = 1,
                UserId = 4,
                QuantityNormal = 14,
                QuantityShiny = 13
            };

            CardCollection collection4 = new CardCollection()
            {
                PokemonId = 3,
                UserId = 5,
                QuantityNormal = 14,
                QuantityShiny = 13
            };

            PokemonCard pokemon1 = new PokemonCard()
            {
                PokemonId = 1,
                RarityId = 10,
                SpriteLink = "",
                SpriteLinkShiny = "",
                PokemonName = ""
            };

            PokemonCard pokemon2 = new PokemonCard()
            {
                PokemonId = 2,
                RarityId = 1,
                SpriteLink = "",
                SpriteLinkShiny = "",
                PokemonName = ""
            };

            PokemonCard pokemon3 = new PokemonCard()
            {
                PokemonId = 3,
                RarityId = 1,
                SpriteLink = "",
                SpriteLinkShiny = "",
                PokemonName = ""
            };

            PokemonCard pokemon4 = new PokemonCard()
            {
                PokemonId = 4,
                RarityId = 1,
                SpriteLink = "",
                SpriteLinkShiny = "",
                PokemonName = ""
            };

            //Act
            using (var context = new P3Context(options))
            {
                //verify that the db was deleted and created anew
                context.Database.EnsureDeleted();//delete any Db from a previous test

                context.Database.EnsureCreated();// create a new Db.. you will need to seed it again.

                LeaderboardModel usertest = new LeaderboardModel(context);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.CardCollections.Add(collection1);
                context.CardCollections.Add(collection2);
                context.CardCollections.Add(collection3);
                context.CardCollections.Add(collection4);
                context.PokemonCards.Add(pokemon1);
                context.PokemonCards.Add(pokemon2);
                context.PokemonCards.Add(pokemon3);
                context.PokemonCards.Add(pokemon4);


                context.SaveChanges();
                //Assert

                List<TopPersentCompletedCollectionModel> list1 = usertest.TopPercentageCompletedCollection(2);

                Assert.NotNull(list1);
                Assert.Equal(2, list1.Count());
            }
        }



        /*========================= TEST 1 GUILLERMO ======================================*/
        [Fact]
        public async Task GetTheTopUserWithMostShiny()
        {
            // Arrange
            CardCollection userMostShiny_1 = new() {
                PokemonId = 1,
                UserId = 1,
                QuantityNormal = 10,
                QuantityShiny = 2,
            
            };

            CardCollection userMostShiny_2 = new()
            {
                PokemonId = 1,
                UserId = 2,
                QuantityNormal = 13,
                QuantityShiny = 8,

            }; 
            
            CardCollection userMostShiny_3 = new()
            {
                PokemonId = 1,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 1,

            };

            // Act
            using (var context = new P3Context(options))
            {
                
                LeaderboardBuissnes leaderTest = new LeaderboardBuissnes(context);

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.CardCollections.Add(userMostShiny_1);
                context.CardCollections.Add(userMostShiny_2); 
                context.CardCollections.Add(userMostShiny_3);

                context.SaveChanges();
                //some method perhaps

                List<CardCollection> list = leaderTest.UserMostShinyPokemon(3);

                // Assert
                Assert.NotNull(list);
                Assert.Equal(3, list.Count());
                Assert.Collection(list,
               info => Assert.Equal(8, info.QuantityShiny),
               info => Assert.Equal(2, info.QuantityShiny),
               info => Assert.Equal(1, info.QuantityShiny)
               );
            }
            
        }




        /*========================= TEST 2 GUILLERMO ======================================*/
        [Fact]
        public async Task GetDisplayMVPTopShinyPlayers()
        {
            // Arrange

            // Set values for CardCollection Table
            CardCollection userMostShiny_1 = new()
            {
                PokemonId = 1,
                UserId = 1,
                QuantityNormal = 10,
                QuantityShiny = 2,

            };

            CardCollection userMostShiny_2 = new()
            {
                PokemonId = 2,
                UserId = 2,
                QuantityNormal = 13,
                QuantityShiny = 8,

            };

            CardCollection userMostShiny_3 = new()
            {
                PokemonId = 1,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 6,

            };

            CardCollection userMostShiny_4 = new()
            {
                PokemonId = 4,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 34,

            };

            CardCollection userMostShiny_5 = new()
            {
                PokemonId = 5,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };

            // Set users table
            User user_1 = new()
            {
                UserId = 1,
                FirstName = "user1",
                LastName = "user1",
                UserName = "user1",
                Password ="user1",
                Email = "user1",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_2 = new()
            {
                UserId = 2,
                FirstName = "user2",
                LastName = "user2",
                UserName = "user2",
                Password = "user2",
                Email = "user2",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_3 = new()
            {
                UserId = 3,
                FirstName = "user3",
                LastName = "user3",
                UserName = "user3",
                Password = "user3",
                Email = "user3",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_4 = new()
            {
                UserId = 4,
                FirstName = "user4",
                LastName = "user4",
                UserName = "user4",
                Password = "user4",
                Email = "user4",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };


            // Act
            using (var context = new P3Context(options))
            {

                LeaderboardBuissnes leaderTest = new LeaderboardBuissnes(context);

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Users.Add(user_1);
                context.Users.Add(user_2);
                context.Users.Add(user_3);
                context.Users.Add(user_4);
                context.SaveChanges();

                context.CardCollections.Add(userMostShiny_1);
                context.CardCollections.Add(userMostShiny_2);
                context.CardCollections.Add(userMostShiny_3);
                context.CardCollections.Add(userMostShiny_4);
                context.CardCollections.Add(userMostShiny_5);
                context.SaveChanges();
                //some method perhaps

                List<MVPShiny> listTest = leaderTest.TopShinyTotal(3);

                // Assert
                Assert.NotNull(listTest);
                Assert.Equal(3, listTest.Count());
                Assert.Collection(listTest,
                    info => Assert.Equal(225, info.TotalShiny),
                    info => Assert.Equal(40, info.TotalShiny),
                    info => Assert.Equal(8, info.TotalShiny)       
               );
            }
        }

        /*========================= TEST 3 GUILLERMO ======================================*/
        [Fact]
        public async Task UsersWithMorePokemonCollected()
        {

            // Arrange
            // Set users table
            User user_1 = new()
            {
                UserId = 1,
                FirstName = "user1",
                LastName = "user1",
                UserName = "user1",
                Password = "user1",
                Email = "user1",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_2 = new()
            {
                UserId = 2,
                FirstName = "user2",
                LastName = "user2",
                UserName = "user2",
                Password = "user2",
                Email = "user2",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_3 = new()
            {
                UserId = 3,
                FirstName = "user3",
                LastName = "user3",
                UserName = "user3",
                Password = "user3",
                Email = "user3",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_4 = new()
            {
                UserId = 4,
                FirstName = "user4",
                LastName = "user4",
                UserName = "user4",
                Password = "user4",
                Email = "user4",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };


            // Set CardCollection
            CardCollection userMostShiny_1 = new()
            {
                PokemonId = 1,
                UserId = 1,
                QuantityNormal = 10,
                QuantityShiny = 2,

            };

            CardCollection userMostShiny_2 = new()
            {
                PokemonId = 2,
                UserId = 2,
                QuantityNormal = 13,
                QuantityShiny = 8,

            };

            CardCollection userMostShiny_3 = new()
            {
                PokemonId = 1,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 6,

            };

            CardCollection userMostShiny_4 = new()
            {
                PokemonId = 4,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 34,

            };

            CardCollection userMostShiny_5 = new()
            {
                PokemonId = 5,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };


            CardCollection userMostShiny_6 = new()
            {
                PokemonId = 7,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };

            CardCollection userMostShiny_7 = new()
            {
                PokemonId = 8,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };


            // Act
            using (var context = new P3Context(options))
            {


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // some method perhaps
                LeaderboardBuissnes leaderTest = new LeaderboardBuissnes(context);

                context.Users.Add(user_1);
                context.Users.Add(user_2);
                context.Users.Add(user_3);
                context.Users.Add(user_4);
                context.SaveChanges();

                context.CardCollections.Add(userMostShiny_1);
                context.CardCollections.Add(userMostShiny_2);
                context.CardCollections.Add(userMostShiny_3);
                context.CardCollections.Add(userMostShiny_4);
                context.CardCollections.Add(userMostShiny_5);
                context.CardCollections.Add(userMostShiny_6); 
                context.CardCollections.Add(userMostShiny_7);
                context.SaveChanges();

                // Assert
                List<UsersCollection> listTest = leaderTest.GetUserTotalCollection(3);

                Assert.NotNull(listTest);
                Assert.Equal(3, listTest.Count());
                Assert.Collection(listTest,
               info => Assert.Equal(3, info.Total_Collection),
               info => Assert.Equal(2, info.Total_Collection),
               info => Assert.Equal(1, info.Total_Collection)
               );
            }

        }

        /*========================= TEST 3 GUILLERMO ======================================*/
        [Fact]
        public async Task GetTotalPokemonsInTheDB() {
            // Arrange

            // Set Pokemons Cards
            PokemonCard card_1 = new PokemonCard()
            {
                PokemonId = 1,
                RarityId = 1,
                SpriteLink = "card1",
                SpriteLinkShiny = "card1",
                PokemonName = "Pika",

            };

            PokemonCard card_2 = new PokemonCard()
            {
                PokemonId = 2,
                RarityId = 2,
                SpriteLink = "card2",
                SpriteLinkShiny = "card2",
                PokemonName = "Chary",

            };

            PokemonCard card_3 = new PokemonCard()
            {
                PokemonId = 3,
                RarityId = 3,
                SpriteLink = "card3",
                SpriteLinkShiny = "card3",
                PokemonName = "Cater",

            };

            PokemonCard card_4 = new PokemonCard()
            {
                PokemonId = 4,
                RarityId = 4,
                SpriteLink = "card4",
                SpriteLinkShiny = "card4",
                PokemonName = "Raychu",

            };

            // Act
            using (var context = new P3Context(options))
            {

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // some method perhaps
                LeaderboardBuissnes leaderTest = new LeaderboardBuissnes(context);

                context.PokemonCards.Add(card_1);
                context.PokemonCards.Add(card_2);
                context.PokemonCards.Add(card_3);
                context.PokemonCards.Add(card_4);

                context.SaveChanges();

                int totalpokemons = leaderTest.GetTotalPokemon();

                // Assert
                Assert.Equal(4, totalpokemons);

            }

        }


        /*========================= TEST 4 GUILLERMO ======================================*/

        [Fact]
        public async Task GetTotalCardAmountUserHave()
        {
            // Arrange
            // Set users table
            User user_1 = new()
            {
                UserId = 1,
                FirstName = "user1",
                LastName = "user1",
                UserName = "user1",
                Password = "user1",
                Email = "user1",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_2 = new()
            {
                UserId = 2,
                FirstName = "user2",
                LastName = "user2",
                UserName = "user2",
                Password = "user2",
                Email = "user2",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_3 = new()
            {
                UserId = 3,
                FirstName = "user3",
                LastName = "user3",
                UserName = "user3",
                Password = "user3",
                Email = "user3",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_4 = new()
            {
                UserId = 4,
                FirstName = "user4",
                LastName = "user4",
                UserName = "user4",
                Password = "user4",
                Email = "user4",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };


            // Set CardCollection
            CardCollection userMostShiny_1 = new()
            {
                PokemonId = 1,
                UserId = 1,
                QuantityNormal = 10,
                QuantityShiny = 2,

            };

            CardCollection userMostShiny_2 = new()
            {
                PokemonId = 2,
                UserId = 2,
                QuantityNormal = 13,
                QuantityShiny = 8,

            };

            CardCollection userMostShiny_3 = new()
            {
                PokemonId = 1,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 6,

            };

            CardCollection userMostShiny_4 = new()
            {
                PokemonId = 4,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 34,

            };

            CardCollection userMostShiny_5 = new()
            {
                PokemonId = 5,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };


            CardCollection userMostShiny_6 = new()
            {
                PokemonId = 7,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };

            CardCollection userMostShiny_7 = new()
            {
                PokemonId = 8,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };


            // Act
            using (var context = new P3Context(options))
            {


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Some method perhaps
                LeaderboardBuissnes leaderTest = new LeaderboardBuissnes(context);

                context.Users.Add(user_1);
                context.Users.Add(user_2);
                context.Users.Add(user_3);
                context.Users.Add(user_4);
                context.SaveChanges();

                context.CardCollections.Add(userMostShiny_1);
                context.CardCollections.Add(userMostShiny_2);
                context.CardCollections.Add(userMostShiny_3);
                context.CardCollections.Add(userMostShiny_4);
                context.CardCollections.Add(userMostShiny_5);
                context.CardCollections.Add(userMostShiny_6);
                context.CardCollections.Add(userMostShiny_7);
                context.SaveChanges();

                // Assert
                List<UsersCollection> listTest = leaderTest.GetUserTotalAmount(3);

                Assert.NotNull(listTest);
                Assert.Equal(3, listTest.Count());
                Assert.Collection(listTest,
               info => Assert.Equal(678, info.Total_Collection),
               info => Assert.Equal(42, info.Total_Collection),
               info => Assert.Equal(21, info.Total_Collection)
               );
            }

        }

        /*========================= TEST 5 GUILLERMO ======================================*/

        [Fact]
        public async Task PercentageOfAllUsersThatCurrentlyOwnThisCard()
        {
            // Arrange
            // Set a card
            PokemonCard card_1 = new PokemonCard()
            {
                PokemonId = 3,
                RarityId = 1,
                SpriteLink = "SP-card_1",
                SpriteLinkShiny = "SP-card_1",
                PokemonName = "Mew"

            };


            // Set users table
            User user_1 = new()
            {
                UserId = 1,
                FirstName = "user1",
                LastName = "user1",
                UserName = "user1",
                Password = "user1",
                Email = "user1",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_2 = new()
            {
                UserId = 2,
                FirstName = "user2",
                LastName = "user2",
                UserName = "user2",
                Password = "user2",
                Email = "user2",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_3 = new()
            {
                UserId = 3,
                FirstName = "user3",
                LastName = "user3",
                UserName = "user3",
                Password = "user3",
                Email = "user3",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            User user_4 = new()
            {
                UserId = 4,
                FirstName = "user4",
                LastName = "user4",
                UserName = "user4",
                Password = "user4",
                Email = "user4",
                CoinBalance = 0,
                AccountLevel = 10,
                TotalCoinsEarned = 10

            };

            // Set CardCollection
            CardCollection userMostShiny_1 = new()
            {
                PokemonId = 1,
                UserId = 1,
                QuantityNormal = 10,
                QuantityShiny = 2,

            };

            CardCollection userMostShiny_2 = new()
            {
                PokemonId = 3,
                UserId = 2,
                QuantityNormal = 13,
                QuantityShiny = 8,

            };

            CardCollection userMostShiny_3 = new()
            {
                PokemonId = 3,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 6,

            };

            CardCollection userMostShiny_4 = new()
            {
                PokemonId = 4,
                UserId = 3,
                QuantityNormal = 1,
                QuantityShiny = 34,

            };

            CardCollection userMostShiny_5 = new()
            {
                PokemonId = 5,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };


            CardCollection userMostShiny_6 = new()
            {
                PokemonId = 7,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };

            CardCollection userMostShiny_7 = new()
            {
                PokemonId = 3,
                UserId = 4,
                QuantityNormal = 1,
                QuantityShiny = 225,

            };


            // Act
            using (var context = new P3Context(options))
            {

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                LeaderboardBuissnes test = new LeaderboardBuissnes(context);

                context.PokemonCards.Add(card_1);
                context.SaveChanges();

                context.Users.Add(user_1);
                context.Users.Add(user_2);
                context.Users.Add(user_3);
                context.Users.Add(user_4);
                context.SaveChanges();

                context.CardCollections.Add(userMostShiny_1);
                context.CardCollections.Add(userMostShiny_2);
                context.CardCollections.Add(userMostShiny_3);
                context.CardCollections.Add(userMostShiny_4);
                context.CardCollections.Add(userMostShiny_5);
                context.CardCollections.Add(userMostShiny_6);
                context.CardCollections.Add(userMostShiny_7);
                context.SaveChanges();

                // Assert
                PercentageOwnCard poc = test.GetPercentageOwnCard("MEW");
                Assert.NotNull(poc);
                Assert.Equal("Mew",poc.PokemonName);
                Assert.Equal(4, poc.Total_Users);
                Assert.Equal(3, poc.TotalQy);
                Assert.Equal(75, poc.Percentage_OwnCard);
                
            }
           
        }




        //##############################################
        /*   public void GetUsersTest()
           {
               string connectionString = GetConnectionString();

               using (TransactionScope ts = new TransactionScope())
               {
                   using (SqlConnection connection =
                       new SqlConnection(connectionString))
                   {
                       connection.Open();
                       DataLayer dataAccessLayer = new DataLayer();

                       DataSet dataSet = dataAccessLayer.GetUsers();
                       AddNewUser("Fred", connection);

                       dataSet = dataAccessLayer.GetUsers();
                       DataRow[] dr = dataSet.Tables[0].Select("[UserName] = 'Fred'");
                       Assert.AreEqual(1, dr.Length);
                   }
               }
           }*/

        //##############################################



    }

}
