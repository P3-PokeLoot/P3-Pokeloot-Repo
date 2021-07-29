using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace BuisinessLayerMethods
{
    public interface ILeaderboardMethods
    {
        List<CardCollection> UserMostShinyPokemon(int number);
        List<MVPShiny> TopShinyTotal(int number);
    }
}
