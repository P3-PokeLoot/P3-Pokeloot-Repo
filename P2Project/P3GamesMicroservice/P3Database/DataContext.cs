using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Models;

namespace P3Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<GameInfo> GameInfoes {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            int itemId = 1;

            var gameInfoes = new List<GameInfo>{
                new GameInfo{
                    Id = itemId++,
                    Description = "Battle pokemon using type advantage to win!",
                    Title = "Pokemon RPS",
                    ImagePath = "",
                    Route = "/Game/RPS"
                },
                new GameInfo{
                    Id = itemId++,
                    Description = "Here is a test game item",
                    Title = "Test game #1",
                    ImagePath = "",
                    Route = "/Game/Test1"
                },
                new GameInfo{
                    Id = itemId++,
                    Description = "Here is another test item",
                    Title = "Test game #2",
                    ImagePath = "",
                    Route = "/Game/Test2"
                }
            };

            modelBuilder.Entity<GameInfo>().HasData(gameInfoes);
        }
    }
}
