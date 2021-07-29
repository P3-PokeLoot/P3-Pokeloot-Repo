using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryModels;
using Microsoft.Extensions.Logging;

namespace BuisinessLayerMethods
{
    public class LeaderboardModel : ILeaderboardMethods 
    {
        public readonly P3Context context;
        private readonly ILogger<LeaderboardModel> logger;
        public LeaderboardModel (P3Context context, ILogger<LeaderboardModel> logger)
        {
            this.logger = logger;
            this.context = context;
        }

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
        /* public LeaderboardModel()
        {
            this.context = new P3Context();
        }*/





        public List<User> TopCurrentBallance(int maxnumber)
        {
            List<User> user = null;
            try
            {
               user = context.Users.OrderByDescending(p => p.CoinBalance).Take(maxnumber).ToList(); 
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return user;
        }

        public List<User> TopEarnedCoins(int maxnumber)
        {
            List<User> user = null;
            try
            {
                user = context.Users.OrderByDescending(p => p.TotalCoinsEarned).Take(maxnumber).ToList();
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return user;
        }


        public List<User> TopSpentCoins(int maxnumber)
        {
            List<User> user = null;
            try
            {
                user = context.Users.OrderByDescending(p => p.TotalCoinsEarned - p.CoinBalance).Take(maxnumber).ToList();
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return user;
        }



    }
}
