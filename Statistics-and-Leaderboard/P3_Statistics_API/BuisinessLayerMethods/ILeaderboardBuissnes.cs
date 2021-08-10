using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace BuisinessLayerMethods
{
    public interface ILeaderboardBuissnes
    {
        List<CardCollection> UserMostShinyPokemon(int number);
        List<MVPShiny> TopShinyTotal(int number);
        List<UserCollection2> GetUserTotalCollection(int number);
        int GetTotalPokemon();
        List<UsersCollection> GetUserTotalAmount(int number);
        PercentageOwnCard GetPercentageOwnCard(string name);
    }
}
