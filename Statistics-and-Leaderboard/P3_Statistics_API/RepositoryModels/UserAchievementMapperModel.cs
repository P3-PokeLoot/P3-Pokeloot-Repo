using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModels
{
    public class UserAchievementMapperModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalAchievements { get; set; }

    }
}
