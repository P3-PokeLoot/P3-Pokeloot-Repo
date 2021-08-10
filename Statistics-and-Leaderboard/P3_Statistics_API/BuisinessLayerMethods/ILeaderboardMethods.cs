using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryModels;

namespace BuisinessLayerMethods
{
    public interface ILeaderboardMethods
    {
        List<UserCoinBalance> TopCurrentBallance(int maxnumber);
        List<UserCoinEarned> TopEarnedCoins(int maxnumber);
        List<UserCoinSpent> TopSpentCoins(int maxnumber);

        List<TopPersentCompletedCollectionModel> TopPercentageCompletedCollection(int maxnumber);



    }
}
