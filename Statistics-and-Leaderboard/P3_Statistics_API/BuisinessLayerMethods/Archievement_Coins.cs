using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryModels;

namespace BuisinessLayerMethods
{
    public class Archievement_Coins : IArchievement_Coins
    {
        private readonly P3Context context;

        public Archievement_Coins(P3Context context)
        {
            this.context = context;
        }


        /// <summary>
        /// Save user archievement of earn 100 coin in the UserAchivement table if is completed return true, false is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserEarn100Coins(int userId) {

            User user = context.Users.Where(x => x.UserId == userId).First();

            if(user.TotalCoinsEarned == 100)
            {
                Console.WriteLine($"User complete this archivement wtih a total of coins earn: {user.TotalCoinsEarned}");

                UserAchievement user_Achievement = new UserAchievement()
                {
                    UserId = user.UserId,
                    AchievementId = 0,
                    Completion = "true"

                };

                context.UserAchievements.Add(user_Achievement);
                return true;
            }
            else
            {
                Console.WriteLine("Archivement not completed yet....");
                return false;
            }
        }




        /// <summary>
        /// Save user archievement of earn 1000 coin in the UserAchivement table if is completed return true, false is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserEarn1000Coins(int userId) {

            User user = context.Users.Where(x => x.UserId == userId).First();

            if (user.TotalCoinsEarned == 1000)
            {
                Console.WriteLine($"User complete this archivement wtih a total of coins earn: {user.TotalCoinsEarned}");

                UserAchievement user_Achievement = new UserAchievement()
                {
                    UserId = user.UserId,
                    AchievementId = 1,
                    Completion = "true"

                };

                context.UserAchievements.Add(user_Achievement);
                return true;
            }
            else
            {
                Console.WriteLine("Archivement not completed yet....");
                return false;
            }

        }



        /// <summary>
        /// Save user archievement of earn 10000 coin in the UserAchivement table if is completed return true, false is not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserEarn10000Coins(int userId) {

            User user = context.Users.Where(x => x.UserId == userId).First();

            if (user.TotalCoinsEarned == 10000)
            {
                Console.WriteLine($"User complete this archivement wtih a total of coins earn: {user.TotalCoinsEarned}");

                UserAchievement user_Achievement = new UserAchievement()
                {
                    UserId = user.UserId,
                    AchievementId = 3,
                    Completion = "true"

                };

                context.UserAchievements.Add(user_Achievement);
                return true;
            }
            else
            {
                Console.WriteLine("Archivement not completed yet....");

                return false;
            }

        }


        /// <summary>
        /// Return a list of all hte archivement of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Achievement> UserAchievements(int userId)
        {
            List<Achievement> userArchievement = null;

            userArchievement = (from user in context.UserAchievements
                                join archievement in context.Achievements
                                on user.AchievementId equals archievement.AchievementId
                                where user.UserId == userId
                                select archievement).ToList();

            return userArchievement;
        }



    }
}
