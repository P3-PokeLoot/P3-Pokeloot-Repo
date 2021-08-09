using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using P3Database;
using System;
using Xunit;

namespace GamesApiTests
{
    public class UnitTest1
    {
        //create in-memory DB for future use (not needed yet)
        DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "TestingDb").Options;
        NullLogger<BusinessModel> nullLogger = new NullLogger<BusinessModel>();

        [Fact]
        public void WhosThatPokemonPass()
        {
            // Arrange
            BusinessModel businessModel = new BusinessModel();
            string result;

            // Act
            result = businessModel.WhosThatPokemonGameAsync().Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void RandomPokemonPass()
        {
            // Arrange
            BusinessModel businessModel = new BusinessModel();
            string result;

            // Act
            result = businessModel.RandomPokemonAsync().Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void RpsWinNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                RpsgameStat rpsgameStat = new RpsgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(rpsgameStat);
                context.SaveChanges();
                result = businessModel.RpsWinAsync(rpsgameStat.UserId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void RpsWinNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.RpsWinAsync(userId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void RpsLostNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                RpsgameStat rpsgameStat = new RpsgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(rpsgameStat);
                context.SaveChanges();
                result = businessModel.RpsLoseAsync(rpsgameStat.UserId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }
        [Fact]
        public void RpsLoseNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.RpsLoseAsync(userId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void WtpWinNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                WtpgameStat wtpgameStat = new WtpgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wtpgameStat);
                context.SaveChanges();
                result = businessModel.WtpWinAsync(wtpgameStat.UserId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void WtpWinNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WtpWinAsync(userId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void WtpLoseNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                WtpgameStat wtpgameStat = new WtpgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wtpgameStat);
                context.SaveChanges();
                result = businessModel.WtpLoseAsync(wtpgameStat.UserId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void WtpLoseNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WtpLoseAsync(userId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void CapWinNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                CapgameStat capgameStat = new CapgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                    AverageCatchChance = 0.5
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(capgameStat);
                context.SaveChanges();
                result = businessModel.CapWinAsync(capgameStat.UserId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void CapWinNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CapWinAsync(userId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void CapLoseNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                CapgameStat capgameStat = new CapgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                    AverageCatchChance = 0.5
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(capgameStat);
                context.SaveChanges();
                result = businessModel.CapLoseAsync(capgameStat.UserId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void CapLoseNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CapLoseAsync(userId).Result;

                // Assert
                Assert.True(result); // result is true if record is successfully updated
            }
        }

        [Fact]
        public void RpsRecordNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                RpsgameStat rpsgameStat = new RpsgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(rpsgameStat);
                context.SaveChanges();
                result = businessModel.RpsRecordAsync(rpsgameStat.UserId).Result;

                // Assert
                Assert.NotNull(result); // result is not null if record is returned
            }
        }

        [Fact]
        public void RpsRecordNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.RpsRecordAsync(userId).Result;

                // Assert
                Assert.Null(result); // result is null if no record is returned
            }
        }

        [Fact]
        public void WtpRecordNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                WtpgameStat wtpgameStat = new WtpgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wtpgameStat);
                context.SaveChanges();
                result = businessModel.WtpRecordAsync(wtpgameStat.UserId).Result;

                // Assert
                Assert.NotNull(result); // result is not null if record is returned
            }
        }

        [Fact]
        public void WtpRecordNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WtpRecordAsync(userId).Result;

                // Assert
                Assert.Null(result); // result is null if no record is returned
            }
        }

        [Fact]
        public void CapRecordNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                CapgameStat capgameStat = new CapgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    GamesWon = 1,
                    AverageCatchChance = 0.5
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(capgameStat);
                context.SaveChanges();
                result = businessModel.CapRecordAsync(capgameStat.UserId).Result;

                // Assert
                Assert.NotNull(result); // result is not null if record is returned
            }
        }

        [Fact]
        public void CapRecordNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                int userId = 1;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CapRecordAsync(userId).Result;

                // Assert
                Assert.Null(result); // result is null if no record is returned
            }
        }
    }
}
