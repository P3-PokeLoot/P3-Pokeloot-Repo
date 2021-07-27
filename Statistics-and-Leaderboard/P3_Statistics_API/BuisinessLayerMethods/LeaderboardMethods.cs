using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    public class LeaderboardMethods: ILeaderboardMethods
    {
        private readonly P3Context context;

        /// <summary>
        /// Constructor for leaderboard class that takes a Db context
        /// </summary>
        /// <param name="context">Db context</param>
        /// 

        public LeaderboardMethods(P3Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes no constructor
        /// </summary>
        public LeaderboardMethods()
        {
            this.context = new P3Context();
        }


        public List<CardCollection> TopTenShinyUsers()
        {
            List<CardCollection> usersShiny = null;

               usersShiny = (from user in context.CardCollections
                       orderby user.QuantityShiny descending
                       select user).Take(10).ToList();

            return usersShiny;
        }


    }
}
