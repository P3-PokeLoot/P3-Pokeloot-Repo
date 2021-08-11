using BuisinessLayerMethods;
using Microsoft.EntityFrameworkCore;
using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace P3_Statisitcs_Testing
{
    public class RarityTest
    {
        DbContextOptions<P3Context> options = new DbContextOptionsBuilder<P3Context>().UseInMemoryDatabase(databaseName: "testingDbRarity").Options;
        //================================List Users of Most Rarity ============================
        [Fact]
        public void UserListByMostRarityCategoryPass()
        {
            using (var context = new P3Context(options))
            {
                // Arrange
                List<UserRarityMapperModel> result = new List<UserRarityMapperModel>();
                User user1 = new User()
                {
                    UserId = 1,
                    UserName = "a"
                };
                User user2 = new User()
                {
                    UserId = 2,
                    UserName = "b"
                };
                CardCollection collection1 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 1,
                    QuantityNormal = 1,
                    QuantityShiny = 0
                };
                CardCollection collection2 = new CardCollection()
                {
                    UserId = 2,
                    PokemonId = 1,
                    QuantityNormal = 1,
                    QuantityShiny = 4
                };
                CardCollection collection3 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 2,
                    QuantityNormal = 10,
                    QuantityShiny = 2
                };
                PokemonCard card1 = new PokemonCard()
                {
                    PokemonId = 1,
                    RarityId = 1
                };
                PokemonCard card2 = new PokemonCard()
                {
                    PokemonId = 2,
                    RarityId = 2
                };
                RarityType rare = new RarityType()
                {
                    RarityId = 1,
                    RarityCategory = "Legendary"
                };
                RarityType rare2 = new RarityType()
                {
                    RarityId = 2,
                    RarityCategory = "Common"
                };
                RarityType rare3 = new RarityType()
                {
                    RarityId = 3,
                    RarityCategory = "Uncommon"
                };
                RarityType rare4 = new RarityType()
                {
                    RarityId = 4,
                    RarityCategory = "Rare"
                };
                RarityType rare5 = new RarityType()
                {
                    RarityId = 5,
                    RarityCategory = "Mythic"
                };


                // Act
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Users.Add(user1);
                context.Users.Add(user2);
                context.CardCollections.Add(collection1);
                context.CardCollections.Add(collection2);
                context.CardCollections.Add(collection3);
                context.PokemonCards.Add(card1);
                context.PokemonCards.Add(card2);
                context.RarityTypes.Add(rare);
                context.RarityTypes.Add(rare2);
                context.RarityTypes.Add(rare3);
                context.RarityTypes.Add(rare4);
                context.RarityTypes.Add(rare5);

                context.SaveChanges();
                RarityMethods r = new RarityMethods(context);
                result = r.UserListByMostRarityCategory("Legendary", 100);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(5, result.ElementAt(0).Quantity);
            }
        }
        [Fact]
        public void UserListByMostRarityCategoryNull()
        {
            using (var context = new P3Context(options))
            {
                // Arrange
                List<UserRarityMapperModel> result = new List<UserRarityMapperModel>();
                User user1 = new User();
                //context.Users.Add(user1);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.SaveChanges();
                RarityMethods r = new RarityMethods(context);
                result = r.UserListByMostRarityCategory("Legendary", 100);
                Assert.Empty(result);
            }
        }

        [Fact]
        public void PercentOfRarityCategoryPass()
        {
            using (var context = new P3Context(options))
            {
                // Arrange
                int result = 0;
                User user1 = new User()
                {
                    UserId = 1,
                    UserName = "a"
                };
                CardCollection collection1 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 1,
                    QuantityNormal = 4
                };
                CardCollection collection2 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 2,
                    QuantityNormal = 1
                };
                CardCollection collection3 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 3,
                    QuantityNormal = 1
                };
                PokemonCard card1 = new PokemonCard()
                {
                    PokemonId = 1,
                    RarityId = 1
                };
                PokemonCard card2 = new PokemonCard()
                {
                    PokemonId = 2,
                    RarityId = 1
                };
                PokemonCard card3 = new PokemonCard()
                {
                    PokemonId = 3,
                    RarityId = 2
                };
                PokemonCard card4 = new PokemonCard()
                {
                    PokemonId = 4,
                    RarityId = 1
                };
                RarityType rare = new RarityType()
                {
                    RarityId = 1,
                    RarityCategory = "Legendary"
                };
                RarityType rare2 = new RarityType()
                {
                    RarityId = 2,
                    RarityCategory = "Common"
                };

                // Act
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Users.Add(user1);
                context.CardCollections.Add(collection1);
                context.CardCollections.Add(collection2);
                context.CardCollections.Add(collection3);
                context.PokemonCards.Add(card1);
                context.PokemonCards.Add(card2);
                context.PokemonCards.Add(card3);
                context.PokemonCards.Add(card4);
                context.RarityTypes.Add(rare);
                context.RarityTypes.Add(rare2);

                context.SaveChanges();
                RarityMethods r = new RarityMethods(context);
                result = r.PercentOfRarityCategory(1, "Legendary");

                // Assert
                Assert.Equal(67, result);
            }
        }

        [Fact]
        public void TotalRarityCategoryForUserPass()
        {
            using (var context = new P3Context(options))
            {
                // Arrange
                int result = 0;
                User user1 = new User()
                {
                    UserId = 1,
                    UserName = "a"
                };
                CardCollection collection1 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 1,
                    QuantityNormal = 4
                };
                CardCollection collection2 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 2,
                    QuantityNormal = 1
                };
                CardCollection collection3 = new CardCollection()
                {
                    UserId = 1,
                    PokemonId = 3,
                    QuantityNormal = 1
                };
                PokemonCard card1 = new PokemonCard()
                {
                    PokemonId = 1,
                    RarityId = 1
                };
                PokemonCard card2 = new PokemonCard()
                {
                    PokemonId = 2,
                    RarityId = 1
                };
                PokemonCard card3 = new PokemonCard()
                {
                    PokemonId = 3,
                    RarityId = 2
                };
                PokemonCard card4 = new PokemonCard()
                {
                    PokemonId = 4,
                    RarityId = 1
                };
                RarityType rare = new RarityType()
                {
                    RarityId = 1,
                    RarityCategory = "Legendary"
                };
                RarityType rare2 = new RarityType()
                {
                    RarityId = 2,
                    RarityCategory = "Common"
                };

                // Act
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Users.Add(user1);
                context.CardCollections.Add(collection1);
                context.CardCollections.Add(collection2);
                context.CardCollections.Add(collection3);
                context.PokemonCards.Add(card1);
                context.PokemonCards.Add(card2);
                context.PokemonCards.Add(card3);
                context.PokemonCards.Add(card4);
                context.RarityTypes.Add(rare);
                context.RarityTypes.Add(rare2);

                context.SaveChanges();
                RarityMethods r = new RarityMethods(context);
                result = r.TotalRarityCategoryForUser(1, "Legendary");

                // Assert
                Assert.Equal(5,result);
            }
        }
    }
}
