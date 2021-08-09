using Microsoft.Extensions.Logging;
using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    public class AchievementMethods : IAchievementMethods
    {
        public readonly P3Context context;
        private readonly ILogger<LeaderboardModel> logger;
        public AchievementMethods(P3Context context, ILogger<LeaderboardModel> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes a Db context
        /// </summary>
        /// <param name="context">Db context</param>
        public AchievementMethods(P3Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes no constructor
        /// </summary>
        public AchievementMethods()
        {
            this.context = new P3Context();
        }

        /// <summary>
        /// Returns List of UsersRarityMapperMethods by who has the most Cards of a Rarity Category
        /// </summary>
        public List<UserAchievementMapperModel> UserListByMostAchievements(int maxnumber)
        {
            List<UserAchievementMapperModel> result = new List<UserAchievementMapperModel>();
            var totalAchievementsList = (from u in context.UserAchievements
                                         where u.Completion == "true"
                                         group u by u.UserId into GroupModel
                                         orderby GroupModel.Count() descending
                                         select new
                                         {
                                             UserId = GroupModel.Key,
                                             UserName = context.Users.Where(x => x.UserId == GroupModel.Key).FirstOrDefault().UserName,
                                             TotalAchievements = GroupModel.Count()
                                         }
                                         ).Take(maxnumber).ToList();
            foreach (var item in totalAchievementsList)
            {
                UserAchievementMapperModel model = new UserAchievementMapperModel()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    TotalAchievements = item.TotalAchievements
                };
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// Returns Percent of User's Total Cards of a Rarity Category
        /// </summary>
        public int PercentOfAchievementType(string achievementName)
        {
            var result = 0;
            // Find Achievement Id for Selected Type 
            int id = context.Achievements.Where(x => x.AchievementName == achievementName).FirstOrDefault().AchievementId;
            // Total Users Who Passed the Achievements
            decimal totalPassedAchievement = context.UserAchievements.Where(x => x.AchievementId == id).Where(x => x.Completion == "true").Count();
            // Total Users
            decimal totalUsers = context.Users.Count();
            // Percent
            result = (int)Math.Round((totalPassedAchievement / totalUsers) * 100);
            return result;
        }
    }
}
