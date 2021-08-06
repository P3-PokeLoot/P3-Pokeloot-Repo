using BusinessLayer;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public void WhosThatPokemonPass()
        {
            // Arrange
            BusinessModel businessModel = new BusinessModel();
            string result;

            // Act
            result = businessModel.WhosThatPokemonGame();

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
            result = businessModel.RandomPokemon();

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(rpsgameStat);
                context.SaveChanges();
                result = businessModel.RpsWin(rpsgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.RpsWin(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(rpsgameStat);
                context.SaveChanges();
                result = businessModel.RpsLose(rpsgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.RpsLose(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wtpgameStat);
                context.SaveChanges();
                result = businessModel.WtpWin(wtpgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WtpWin(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wtpgameStat);
                context.SaveChanges();
                result = businessModel.WtpLose(wtpgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WtpLose(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(capgameStat);
                context.SaveChanges();
                result = businessModel.CapWin(capgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CapWin(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(capgameStat);
                context.SaveChanges();
                result = businessModel.CapLose(capgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CapLose(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(rpsgameStat);
                context.SaveChanges();
                result = businessModel.RpsRecord(rpsgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.RpsRecord(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wtpgameStat);
                context.SaveChanges();
                result = businessModel.WtpRecord(wtpgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WtpRecord(userId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(capgameStat);
                context.SaveChanges();
                result = businessModel.CapRecord(capgameStat.UserId);

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
                BusinessModel businessModel = new BusinessModel(context);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CapRecord(userId);

                // Assert
                Assert.Null(result); // result is null if no record is returned
            }
        }
    }
}
