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
    public class AchievementTest
    {
        DbContextOptions<P3Context> options = new DbContextOptionsBuilder<P3Context>().UseInMemoryDatabase(databaseName: "testingDbAchievement").Options;

        [Fact]
        public void UserListByMostAchievementsPass()
        {
            // Assign
            User user1 = new User() { UserId = 1, UserName = "Guillermo" };
            User user2 = new User() { UserId = 2, UserName = "Qais" };
            User user3 = new User() { UserId = 3, UserName = "Greg" };
            User user4 = new User() { UserId = 4, UserName = "Chrisrian" };
            Achievement achievement1 = new Achievement()
            {
                AchievementId = 1,
                AchievementName = "achievement1"
            };
            Achievement achievement2 = new Achievement()
            {
                AchievementId = 2,
                AchievementName = "achievement2"
            };
            Achievement achievement3 = new Achievement()
            {
                AchievementId = 3,
                AchievementName = "achievement3"
            };
            UserAchievement userAchievement1 = new UserAchievement()
            {
                UserId = 1,
                AchievementId = 1,
                Completion = "true"
            };
            UserAchievement userAchievement2 = new UserAchievement()
            {
                UserId = 1,
                AchievementId = 2,
                Completion = "true"
            };
            UserAchievement userAchievement3 = new UserAchievement()
            {
                UserId = 1,
                AchievementId = 3,
                Completion = "false"
            };
            UserAchievement userAchievement4 = new UserAchievement()
            {
                UserId = 2,
                AchievementId = 1,
                Completion = "false"
            };
            UserAchievement userAchievement5 = new UserAchievement()
            {
                UserId = 2,
                AchievementId = 2,
                Completion = "false"
            };
            UserAchievement userAchievement6 = new UserAchievement()
            {
                UserId = 2,
                AchievementId = 3,
                Completion = "true"
            };
            UserAchievement userAchievement7 = new UserAchievement()
            {
                UserId = 3,
                AchievementId = 1,
                Completion = "true"
            };
            UserAchievement userAchievement8 = new UserAchievement()
            {
                UserId = 3,
                AchievementId = 2,
                Completion = "false"
            };
            UserAchievement userAchievement9 = new UserAchievement()
            {
                UserId = 3,
                AchievementId = 3,
                Completion = "false"
            };
            UserAchievement userAchievement10 = new UserAchievement()
            {
                UserId = 4,
                AchievementId = 1,
                Completion = "true"
            };
            UserAchievement userAchievement11 = new UserAchievement()
            {
                UserId = 4,
                AchievementId = 2,
                Completion = "true"
            };
            UserAchievement userAchievement12 = new UserAchievement()
            {
                UserId = 4,
                AchievementId = 3,
                Completion = "true"
            };

            // Act
            using (var context = new P3Context(options))
            {
                //verify that the db was deleted and created anew
                context.Database.EnsureDeleted();//delete any Db from a previous test
                context.Database.EnsureCreated();// create a new Db.. you will need to seed it again.

                AchievementMethods achievementMethods = new AchievementMethods(context);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.Users.Add(user4);
                context.Achievements.Add(achievement1);
                context.Achievements.Add(achievement2);
                context.Achievements.Add(achievement3);
                context.UserAchievements.Add(userAchievement1);
                context.UserAchievements.Add(userAchievement2);
                context.UserAchievements.Add(userAchievement3);
                context.UserAchievements.Add(userAchievement4);
                context.UserAchievements.Add(userAchievement5);
                context.UserAchievements.Add(userAchievement6);
                context.UserAchievements.Add(userAchievement7);
                context.UserAchievements.Add(userAchievement8);
                context.UserAchievements.Add(userAchievement9);
                context.UserAchievements.Add(userAchievement10);
                context.UserAchievements.Add(userAchievement11);
                context.UserAchievements.Add(userAchievement12);

                context.SaveChanges();

                List<UserAchievementMapperModel> result = achievementMethods.UserListByMostAchievements(4);
                // Assert
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void PercentOfAchievementTypePass()
        {
            // Arrange
            User user1 = new User() { UserId = 1 };
            User user2 = new User() { UserId = 2 };
            User user3 = new User() { UserId = 3 };
            User user4 = new User() { UserId = 4 };
            Achievement achievement = new Achievement()
            {
                AchievementId = 1,
                AchievementName = "achievement"
            };
            UserAchievement userAchievement1 = new UserAchievement()
            {
                UserId = 1,
                AchievementId = 1,
                Completion = "true"
            };
            UserAchievement userAchievement2 = new UserAchievement()
            {
                UserId = 2,
                AchievementId = 1,
                Completion = "true"
            };
            UserAchievement userAchievement3 = new UserAchievement()
            {
                UserId = 3,
                AchievementId = 1,
                Completion = "false"
            };
            UserAchievement userAchievement4 = new UserAchievement()
            {
                UserId = 4,
                AchievementId = 1,
                Completion = "false"
            };

            // Act
            using (var context = new P3Context(options))
            {
                //verify that the db was deleted and created anew
                context.Database.EnsureDeleted();//delete any Db from a previous test
                context.Database.EnsureCreated();// create a new Db.. you will need to seed it again.

                AchievementMethods achievementMethods = new AchievementMethods(context);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.Users.Add(user4);
                context.Achievements.Add(achievement);
                context.UserAchievements.Add(userAchievement1);
                context.UserAchievements.Add(userAchievement2);
                context.UserAchievements.Add(userAchievement3);
                context.UserAchievements.Add(userAchievement4);

                context.SaveChanges();

                int result = achievementMethods.PercentOfAchievementType("achievement");
                // Assert
                Assert.Equal(50, result);
            }
        }
    }
}
