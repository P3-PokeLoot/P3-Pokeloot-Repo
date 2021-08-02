using Microsoft.EntityFrameworkCore;
using RepositoryModels;
using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BuisinessLayerMethods;

namespace P3_Statisitcs_Testing
{
    public class DemoTest
    {
        DbContextOptions<P3Context> options = new DbContextOptionsBuilder<P3Context>().UseInMemoryDatabase(databaseName: "testingDb").Options;
        /*[Fact]
        public async Task SomeTestMethodAsync()
        {
            //Arrange
            User user = new()
            {
                UserId = 1,
            };

            //Act
            //bool result = false;
            using (var context = new P3Context(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //some method perhaps

                context.SaveChanges();
            }

            //Assert
            using (var context = new P3Context(options))
            {
                //assert things here
                //Assert.True(result);
            }
        }*/



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
                
                LeaderboardMethods leaderTest = new LeaderboardMethods(context);

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

            //Set values for CardCollection Table
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

            //Set users table
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

                LeaderboardMethods leaderTest = new LeaderboardMethods(context);

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
            //Set users table
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


            //Set CardCollection
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


            //Act
            using (var context = new P3Context(options))
            {


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //some method perhaps
                LeaderboardMethods leaderTest = new LeaderboardMethods(context);

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
            //Arrange

            //Set Pokemons Cards
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

            //Act
            using (var context = new P3Context(options))
            {

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //some method perhaps
                LeaderboardMethods leaderTest = new LeaderboardMethods(context);

                context.PokemonCards.Add(card_1);
                context.PokemonCards.Add(card_2);
                context.PokemonCards.Add(card_3);
                context.PokemonCards.Add(card_4);

                context.SaveChanges();

                int totalpokemons = leaderTest.GetTotalPokemon();

                //Assert
                Assert.Equal(4, totalpokemons);

            }

        }


        /*========================= TEST 4 GUILLERMO ======================================*/

        [Fact]
        public async Task GetTotalCardAmountUserHave()
        {
            // Arrange
            //Set users table
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


            //Set CardCollection
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


            //Act
            using (var context = new P3Context(options))
            {


                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //some method perhaps
                LeaderboardMethods leaderTest = new LeaderboardMethods(context);

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
    }
}
