using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BuisinessLayerMethods
{
    public class LeaderboardBuissnes : ILeaderboardBuissnes
    {
        private readonly P3Context context;

        /// <summary>
        /// Constructor for leaderboard class that takes a Db context
        /// </summary>
        /// <param name="context">Db context</param>
        /// 
        public LeaderboardBuissnes(P3Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Constructor for leaderboard class that takes no constructor
        /// </summary>
        public LeaderboardBuissnes()
        {
            this.context = new P3Context();
        }


        /// <summary>
        /// Retrive the users with most shinys for pokmemon 
        /// </summary>
        /// <param name="userMost"></param>
        /// <returns></returns>
        public List<CardCollection> UserMostShinyPokemon(int userMost)
        {
            List<CardCollection> usersShiny = null;

            usersShiny = (from user in context.CardCollections
                          orderby user.QuantityShiny descending
                          select user).Take(userMost).ToList();

            return usersShiny;
        }



        /// <summary>
        /// Display the Sum of all shinys a user has
        /// </summary>
        /// <param name="topUser"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Display the amount of unique pokemons a user have in the collection
        /// </summary>
        /// <param name="topUsers"></param>
        /// <returns></returns>
        public List<UserCollection2> GetUserTotalCollection(int topUsers)
        {
            List<User> table_1 = context.Users.ToList();
            List<CardCollection> table_2 = context.CardCollections.ToList();

            var userCollection = (from table2 in table_2
                                  join table1 in table_1
                                  on table2.UserId equals table1.UserId
                                  group table2 by table1.UserId into temp

                                  select new UserCollection2
                                  {
                                      UserId = temp.Key,
                                      FirstName = temp.First().User.FirstName,
                                      LastName = temp.First().User.LastName,
                                      Total_Unique_Cards = temp.Count(x => x.PokemonId >= 0) 

                                  }).OrderByDescending(x => x.Total_Unique_Cards).Take(topUsers).ToList();

            return userCollection;
        }




        /// <summary>
        /// Get the total amount of cards a user have
        /// </summary>
        /// <param name="topUser"></param>
        /// <returns></returns>
        public List<UsersCollection> GetUserTotalAmount(int topUser) {

            List<User> table_1 = context.Users.ToList();
            List<CardCollection> table_2 = context.CardCollections.ToList();

            var userAmount = (from table2 in table_2
                                  join table1 in table_1
                                  on table2.UserId equals table1.UserId
                                  group table2 by table1.UserId into temp

                                  select new UsersCollection
                                  {
                                      UserId = temp.Key,
                                      FirstName = temp.First().User.FirstName,
                                      LastName = temp.First().User.LastName,
                                      Total_Collection = temp.Sum(x => x.QuantityShiny) + temp.Sum(x => x.QuantityNormal)

                                  }).OrderByDescending(x => x.Total_Collection).Take(topUser).ToList();
            return userAmount;

        }



        /// <summary>
        ///  Users can see the percentage of all users that currently own this pokemon card.
        /// </summary>
        /// <param name="pokemon_name"></param>
        /// <returns></returns>
        public PercentageOwnCard GetPercentageOwnCard(string pokemon_name)
        {
            //Search for the card by name
            PokemonCard pokemon_card = context.PokemonCards.Where(x => x.PokemonName.ToLower().Equals(pokemon_name.ToLower())).First();

            double x = context.Users.Count();

            double total_users_own = context.CardCollections.Where(x => x.PokemonId == pokemon_card.PokemonId).Count();

            
            PercentageOwnCard pokemon_percentage = new PercentageOwnCard() {

                PokemonId = pokemon_card.PokemonId,
                PokemonName = pokemon_card.PokemonName,
                RarityId = pokemon_card.RarityId,
                SpriteLink = pokemon_card.SpriteLink,
                SpriteLinkShiny = pokemon_card.SpriteLinkShiny,
                Total_Users = context.Users.Count(),
                TotalQy = total_users_own,
                Percentage_OwnCard = (total_users_own / x)*100

            };
            return pokemon_percentage;
        }

        /// <summary>
        /// Display the total amount of unique pokemons in the database that exist
        /// </summary>
        /// <returns></returns>
        public int GetTotalPokemon()
        {

            int totalPokemon = context.PokemonCards.Count();

            return totalPokemon;
            
        }
    }
}

