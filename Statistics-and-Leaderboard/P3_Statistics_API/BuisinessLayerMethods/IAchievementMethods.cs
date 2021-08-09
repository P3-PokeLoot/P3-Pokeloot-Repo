using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    public interface IAchievementMethods
    {
        List<UserAchievementMapperModel> UserListByMostAchievements(int maxnumber);
        int PercentOfAchievementType(string achievementName);
    }
}
