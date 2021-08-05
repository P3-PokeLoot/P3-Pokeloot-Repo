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

                List<User> list1 = usertest.TopCurrentBallance(2);

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

                List<User> list1 = usertest.TopEarnedCoins(2);

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

                List<User> list1 = usertest.TopSpentCoins(2);

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
