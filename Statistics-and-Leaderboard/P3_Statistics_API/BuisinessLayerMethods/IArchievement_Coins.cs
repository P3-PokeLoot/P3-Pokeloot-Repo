using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryModels;

namespace BuisinessLayerMethods
{
    public interface IArchievement_Coins
    {

        bool UserEarn100Coins(int number);
        bool UserEarn1000Coins(int number);
        bool UserEarn10000Coins(int number);
        List<Achievement> UserAchievements(int userId);
    }
}
