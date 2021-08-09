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


        public bool UserEarn100Coins(int userId) {

            User user = context.Users.Where(x => x.UserId == userId).First();

            if(user.TotalCoinsEarned >= 100)
            {
                Console.WriteLine($"User complete this archivement wtih a total of coins earn: {user.TotalCoinsEarned}");

                context.UserAchievements.Add();
                return true;
            }
            else
            {

                return false;
            }
        
        }

        public bool UserEarn1000Coins(int userId) { }

        public bool UserEarn10000Coins(int userId) { }
    }
}
