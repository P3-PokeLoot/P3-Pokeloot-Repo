using BusinessLayer;
using Microsoft.EntityFrameworkCore;
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
    }
}
