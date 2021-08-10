using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace BuisinessLayerMethods
{
    public interface ILeaderboardBusiness
    {
        List<CardCollection> UserMostShinyPokemon(int userMost);
        List<MVPShiny> TopShinyTotal(int topUser);
        List<UsersCollection> GetUserTotalCollection(int topUsers);
        int GetTotalPokemon();
        List<UsersCollection> GetUserTotalAmount(int topUser);
        PercentageOwnCard GetPercentageOwnCard(string pokemon_name);
    }
}
