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
        /// TopCurrentBallance will return a list of top X users that they have the highest Ballance of coins 
        /// by ordering the databse descending based on their balance then we take the first X number (maxnumber) 
        /// </summary>
        public List<UserCoinBalance> TopCurrentBallance(int maxnumber)
        {
            List<UserCoinBalance> result = new List<UserCoinBalance>();
            try
            {
               var user = context.Users.OrderByDescending(p => p.CoinBalance).Take(maxnumber).ToList();
                if (user!=null) {
                    foreach (var item in user)
                    {
                        UserCoinBalance model = new UserCoinBalance()
                        {
                            UserId = item.UserId,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            CoinBalance = item.CoinBalance
                        };
                        result.Add(model);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }

            return result;
        }


        /// <summary>
        /// TopEarnedCoins will return a list of top X users that they have the highest Earned Coins 
        /// by ordering the databse descending based on their Total Coins Earned then we take the first X number (maxnumber) 
        /// </summary>
        public List<UserCoinEarned> TopEarnedCoins(int maxnumber)
        {
            List<UserCoinEarned> result = new List<UserCoinEarned>();
            try
            {
                var user = context.Users.OrderByDescending(p => p.TotalCoinsEarned).Take(maxnumber).ToList();
                foreach (var item in user)
                {
                    UserCoinEarned model = new UserCoinEarned()
                    {
                        UserId = item.UserId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        TotalCoinsEarned = item.TotalCoinsEarned
                    };
                    result.Add(model);
                }

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return result;
        }

        /// <summary>
        /// TopSpentCoins will return a list of top X users that they have the highest Spent Coins 
        /// by ordering the databse descending based on their Total Coins Earned minus their Current Coins Balance then we take the first X number (maxnumber) 
        /// </summary>

        public List<UserCoinSpent> TopSpentCoins(int maxnumber)
        {
            List<UserCoinSpent> result = new List<UserCoinSpent>();
            try
            {
                var user = context.Users.OrderByDescending(p => p.TotalCoinsEarned - p.CoinBalance).Take(maxnumber).ToList();
                foreach (var item in user)
                {
                    UserCoinSpent model = new UserCoinSpent()
                    {
                        UserId = item.UserId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        TotalCoinsSpent=item.TotalCoinsEarned-item.CoinBalance
                         
                    };
                    result.Add(model);
                }

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return result;
        }

        /// <summary>
        /// TopPercentageCompletedCollection will return a list of top X users that they have the highest Percentage collected cards from CARDCOLLECTIONS Table 
        /// compared to the total number of cards in the PokemonCards table 
        /// by ordering the databse descending based on their Total Coins Earned minus their Current Coins Balance then we take the first X number (maxnumber) 
        /// </summary>
        public List<TopPersentCompletedCollectionModel> TopPercentageCompletedCollection(int maxnumber)

        {

          

            //Convert the Top X Number to string for concatination purpose
            
            List<TopPersentCompletedCollectionModel> dataResult = null;
            try
            {
                double totalPokemons = context.PokemonCards.Count();
                var cardcol = context.CardCollections.ToList();
                var userlist = context.Users.ToList();


                dataResult = (from Tuser in userlist
                              join Tcard in cardcol
                              on Tuser.UserId equals Tcard.UserId
                              group Tcard by Tuser.UserId into temptable
                              select new TopPersentCompletedCollectionModel
                              {
                                  UserId = temptable.Key,
                                  FirstName = temptable.First().User.FirstName,
                                  LastName = temptable.First().User.LastName,
                                  Card_collection = ((temptable.Distinct().Count(x => x.PokemonId >= 0) / totalPokemons) * 100)
                              }).OrderByDescending(x => x.Card_collection).Take(maxnumber).ToList();

            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
            return dataResult;
        }
    }
}
