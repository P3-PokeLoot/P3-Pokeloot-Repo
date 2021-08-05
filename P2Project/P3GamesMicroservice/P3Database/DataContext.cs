using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                    Title = "Pokemon Rock Paper Scissors",
                    ImagePath = "",
                    Route = "/Game/RPS"
                },
                new GameInfo{
                    Id = itemId++,
                    Description = "Guess the Pokemon based on the silhouette!",
                    Title = "Who's That Pokemon!",
                    ImagePath = "",
                    Route = "/Game/WhosThatPokemon"
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
