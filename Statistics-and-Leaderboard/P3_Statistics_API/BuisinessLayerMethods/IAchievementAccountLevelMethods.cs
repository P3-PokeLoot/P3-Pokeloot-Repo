﻿using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    public interface IAchievementAccountLevelMethods
    {
        UserAchievement ReachAccountLevelTen(int userId, int accountLevel);
    }
}
