using RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayerMethods
{
    public interface IRarityMethods
    {
        List<UserRarityMapperModel> UserListByMostRarityCategory(string rarityCategory, int maxnumber);
        int PercentOfRarityCategory(int userId, string rarityCategory);
        int TotalRarityCategoryForUser(int userId, string rarityCategory);

    }
}
