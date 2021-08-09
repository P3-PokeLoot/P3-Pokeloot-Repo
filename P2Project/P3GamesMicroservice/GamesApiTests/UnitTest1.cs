using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using P3Database;
using System;
using System.Collections.Generic;
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

        [Fact]
        public void WamPlayedNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int score = 2;
                WamgameStat wamgameStat = new WamgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    HighScore = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wamgameStat);
                context.SaveChanges();
                result = businessModel.WamPlayedAsync(wamgameStat.UserId, score).Result;

                // Assert
                Assert.True(result); // result is true if record updated
            }
        }

        [Fact]
        public void WamPlayedNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                int userId = 1;
                int score = 2;
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.WamPlayedAsync(userId, score).Result;

                // Assert
                Assert.True(result); // result is true if record createed
            }
        }

        [Fact]
        public void WamHighScoreNotNullPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                string result;
                WamgameStat wamgameStat = new WamgameStat()
                {
                    UserId = 1,
                    TotalGamesPlayed = 1,
                    HighScore = 1,
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(wamgameStat);
                context.SaveChanges();
                result = businessModel.WamHighScoreAsync(wamgameStat.UserId).Result;

                // Assert
                Assert.NotNull(result); // result is not null if record is returned
            }
        }

        [Fact]
        public void WamHighScoreNullPass()
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
                result = businessModel.WamHighScoreAsync(userId).Result;

                // Assert
                Assert.Null(result); // result is null if no record is returned
            }
        }

        [Fact]
        public void GameInfoListPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                List<GameInfo> result;
                GameInfo gameInfo1 = new GameInfo()
                {
                    Description = "A cool game",
                    Title = "Cool game",
                    ImagePath = "path",
                    Route = "route"
                };
                GameInfo gameInfo2 = new GameInfo()
                {
                    Description = "A cool game 2",
                    Title = "Cool game 2",
                    ImagePath = "path 2",
                    Route = "route 2"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(gameInfo1);
                context.Add(gameInfo2);
                context.SaveChanges();
                result = businessModel.GameInfoListAsync().Result;

                // Assert
                Assert.NotNull(result); // result is not null if list is returned
            }
        }

        [Fact]
        public void AddGameInfoPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                bool result;
                GameInfo gameInfo = new GameInfo()
                {
                    Description = "A cool game",
                    Title = "Cool game",
                    ImagePath = "path",
                    Route = "route"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.AddGameInfoAsync(gameInfo).Result;

                // Assert
                Assert.True(result); // result is true if game added
            }
        }

        [Fact]
        public void CreateGamePass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                GameInfo result;
                GameDetail gameDetail = new GameDetail()
                {
                    Description = "A cool game",
                    Title = "Cool game",
                    ImageName = "Neat image",
                    Route = "route",
                    ImageSource = "source",
                    //ImageFile = ?
                    OldImageName = "Neat image's old name"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                result = businessModel.CreateGameAsync(gameDetail).Result;

                // Assert
                Assert.NotNull(result); // result is not null if game created
            }
        }

        [Fact]
        public void GetGameInfoListPass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                List<GameDetail> result;
                GameInfo gameInfo1 = new GameInfo()
                {
                    Description = "A cool game",
                    Title = "Cool game",
                    ImagePath = "path",
                    Route = "route"
                };
                GameInfo gameInfo2 = new GameInfo()
                {
                    Description = "A cool game 2",
                    Title = "Cool game 2",
                    ImagePath = "path 2",
                    Route = "route 2"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(gameInfo1);
                context.Add(gameInfo2);
                context.SaveChanges();
                result = businessModel.GetGameInfoListAsync().Result;

                // Assert
                Assert.NotNull(result); // result is not null if list returned
            }
        }

        [Fact]
        public void DeleteGamePass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                GameInfo result;
                GameInfo testInfo;
                GameDetail gameDetail = new GameDetail()
                {
                    Description = "A cool game",
                    Title = "Cool game",
                    ImageName = "Neat image",
                    Route = "route",
                    ImageSource = "source",
                    //ImageFile = ?
                    OldImageName = "Neat image's old name"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                testInfo = businessModel.CreateGameAsync(gameDetail).Result;
                result = businessModel.DeleteGameAsync(testInfo.Id).Result;

                // Assert
                Assert.NotNull(result); // result is not null if game deleted
            }
        }

        [Fact]
        public void ModifyGamePass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                GameInfo result;
                GameDetail gameDetail = new GameDetail()
                {
                    Id = 1,
                    Description = "A cool game",
                    Title = "Cool game",
                    ImageName = "Neat image",
                    Route = "route",
                    ImageSource = "source",
                    //ImageFile = ?
                    OldImageName = "Neat image's old name"
                };
                GameDetail gameDetailUpdated = new GameDetail()
                {
                    Id = 1,
                    Description = "A new description",
                    Title = "Cool game",
                    ImageName = "Neat image",
                    Route = "route",
                    ImageSource = "source",
                    //ImageFile = ?
                    OldImageName = "Neat image's old name"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                businessModel.CreateGameAsync(gameDetail);
                result = businessModel.ModifyGameAsync(gameDetailUpdated).Result;

                // Assert
                Assert.NotNull(result); // result is not null if game modified
            }
        }

        [Fact]
        public void SingleGamePass()
        {
            using (var context = new DataContext(options))
            {
                // Arrange
                GameDetail result;
                GameInfo gameInfo = new GameInfo()
                {
                    Description = "A cool game",
                    Title = "Cool game",
                    ImagePath = "path",
                    Route = "route"
                };
                BusinessModel businessModel = new BusinessModel(context, nullLogger);

                // Act
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                context.Add(gameInfo);
                context.SaveChanges();
                result = businessModel.SingleGameAsync(gameInfo.Id).Result;

                // Assert
                Assert.NotNull(result); // result is not null 
            }
        }
    }
}
