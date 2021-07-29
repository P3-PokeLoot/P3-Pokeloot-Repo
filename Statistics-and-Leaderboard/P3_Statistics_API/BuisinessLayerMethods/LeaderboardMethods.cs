using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BuisinessLayerMethods
{
    public class LeaderboardMethods : ILeaderboardMethods
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




        public List<MVPShiny> TopShinyTotal(int topUser)
        {

            List<CardCollection> table_1 = context.CardCollections.ToList();
            List<User> table_2 = context.Users.ToList();

            var usersShiny = (from table1 in table_1
                              join table2 in table_2
                              on table1.UserId equals table2.UserId
                              group table1 by table2.UserId into temp
                             
                              select new MVPShiny
                              {
                                  UserId = temp.Key,
                                  FirstName = temp.First().User.FirstName,
                                  LastName = temp.First().User.LastName,
                                  AccountLevel = temp.First().User.AccountLevel,
                                  TotalShiny = temp.Sum(x => x.QuantityShiny)
                              }).OrderByDescending(x => x.TotalShiny).Take(topUser).ToList();
     
            return usersShiny;

        }

    }
}

