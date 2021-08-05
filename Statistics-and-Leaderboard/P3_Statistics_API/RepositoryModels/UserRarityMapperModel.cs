using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModels
{
    public class UserRarityMapperModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalCommon { get; set; }
        public int TotalUncommon { get; set; }
        public int TotalRare { get; set; }
        public int TotalMythic { get; set; }
        public int TotalLegendary {get; set;}
    }
}
