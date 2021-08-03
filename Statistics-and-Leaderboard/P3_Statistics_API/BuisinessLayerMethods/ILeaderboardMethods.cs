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
        List<User> TopCurrentBallance(int maxnumber);
        List<User> TopEarnedCoins(int maxnumber);
        List<User> TopSpentCoins(int maxnumber);

        IEnumerable<TopPersentCompletedCollectionModel> TopPercentageCompletedCollection(int maxnumber);



    }
}
