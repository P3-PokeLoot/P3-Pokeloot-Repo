using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    class LeaderboardModel
    {
        public P3Context context;
        /// <summary>
        /// Constructor for leaderboard class that takes a Db context
        /// </summary>
        /// <param name="context">Db context</param>
        public LeaderboardModel(P3Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes no constructor
        /// </summary>
        public LeaderboardModel()
        {
            this.context = new P3Context();
        }





    }
}
