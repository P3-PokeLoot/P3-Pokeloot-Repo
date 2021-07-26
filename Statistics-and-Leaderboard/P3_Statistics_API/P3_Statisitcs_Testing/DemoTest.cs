using Microsoft.EntityFrameworkCore;
using RepositoryModels;
using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace P3_Statisitcs_Testing
{
    public class DemoTest
    {
        DbContextOptions<P3Context> options = new DbContextOptionsBuilder<P3Context>().UseInMemoryDatabase(databaseName: "testingDb").Options;
        [Fact]
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
        }
    }
}
