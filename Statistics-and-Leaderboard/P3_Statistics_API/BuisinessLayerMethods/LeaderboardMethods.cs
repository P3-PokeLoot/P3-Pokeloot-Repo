using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;


using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLayerMethods
{
    public class LeaderboardModel : ILeaderboardMethods 
    {
        public readonly P3Context context;
        public readonly DbConnection conn;
        private readonly ILogger<LeaderboardModel> logger;
        public LeaderboardModel (P3Context context, ILogger<LeaderboardModel> logger)
        {
            this.logger = logger;
            this.context = context;
            this.conn = context.Database.GetDbConnection();

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




        /// <summary>
        /// TopCurrentBallance will return a list of top X users that they have the highest Ballance of coins 
        /// by ordering the databse descending based on their balance then we take the first X number (maxnumber) 
        /// </summary>
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


        /// <summary>
        /// TopEarnedCoins will return a list of top X users that they have the highest Earned Coins 
        /// by ordering the databse descending based on their Total Coins Earned then we take the first X number (maxnumber) 
        /// </summary>
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

        /// <summary>
        /// TopSpentCoins will return a list of top X users that they have the highest Spent Coins 
        /// by ordering the databse descending based on their Total Coins Earned minus their Current Coins Balance then we take the first X number (maxnumber) 
        /// </summary>

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

        /// <summary>
        /// TopPercentageCompletedCollection will return a list of top X users that they have the highest Percentage collected cards from CARDCOLLECTIONS Table 
        /// compared to the total number of cards in the PokemonCards table 
        /// by ordering the databse descending based on their Total Coins Earned minus their Current Coins Balance then we take the first X number (maxnumber) 
        /// </summary>
        public IEnumerable<TopPersentCompletedCollectionModel> TopPercentageCompletedCollection(int maxnumber)

        {
            //Convert the Top X Number to string for concatination purpose
            string mx = maxnumber.ToString();
            IEnumerable<TopPersentCompletedCollectionModel> dataResult = null;
            try
            {

                string sql = @"select top "+ mx +" (cast(count(distinct(C.PokemonId)) as float) / count(distinct(P.PokemonId))) * 100 as Card_collection , count(distinct(P.PokemonId)) as total , U.UserId , U.FirstName from CardCollection C , Users U , PokemonCards P where C.UserId = U.UserId group by u.UserId , u.FirstName order by Card_collection desc";
                dataResult = conn.Query<TopPersentCompletedCollectionModel>(sql);

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return dataResult;
        }
    }
}
