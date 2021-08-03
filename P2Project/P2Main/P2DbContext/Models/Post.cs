using System;
using System.Collections.Generic;

#nullable disable

namespace P2DbContext.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int? PokemonId { get; set; }
        public DateTime PostTime { get; set; }
        public string PostDescription { get; set; }
        public int? Price { get; set; }
        public bool StillAvailable { get; set; }
        public bool? IsShiny { get; set; }

        public virtual PokemonCard Pokemon { get; set; }
        public virtual DisplayBoard DisplayBoard { get; set; }
    }
}
