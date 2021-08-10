using Microsoft.Extensions.Logging;
using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    public class AchievementAccountLevelMethods : IAchievementAccountLevelMethods
    {
        public readonly P3Context context;
        private readonly ILogger<LeaderboardModel> logger;
        public AchievementAccountLevelMethods(P3Context context, ILogger<LeaderboardModel> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes a Db context
        /// </summary>
        /// <param name="context">Db context</param>
        public AchievementAccountLevelMethods(P3Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes no constructor
        /// </summary>
        public AchievementAccountLevelMethods()
        {
            this.context = new P3Context();
        }

        public UserAchievement ReachAccountLevelTen(int userId, int accountLevel)
        {
            UserAchievement result = new UserAchievement();

            int userLevel = context.Users.Where(x => x.UserId == userId).FirstOrDefault().AccountLevel;
            int achievementId = context.Achievements.Where(x => x.AchievementName == "Grunt: Reach account level 10").FirstOrDefault().AchievementId;
            if (userLevel >= accountLevel)
            {
                result.UserId = userId;
                result.AchievementId = achievementId;
                result.Completion = "true";
            }
            else
            {
                result.UserId = userId;
                result.AchievementId = achievementId;
                result.Completion = "false";
            }

            return result;
        }

    }
}
