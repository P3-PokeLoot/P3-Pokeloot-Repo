using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryModels
{
    public partial class Achievement
    {
        public Achievement()
        {
            UserAchievements = new HashSet<UserAchievement>();
        }

        public int AchievementId { get; set; }
        public string AchievementName { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
