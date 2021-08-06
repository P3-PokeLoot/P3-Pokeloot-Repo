using System;
using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class RpsgameStat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? TotalGamesPlayed { get; set; }
        public int? GamesWon { get; set; }
    }
}
