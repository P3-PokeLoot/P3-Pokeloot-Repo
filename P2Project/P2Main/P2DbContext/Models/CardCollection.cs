using System;
using System.Collections.Generic;

#nullable disable

namespace P2DbContext.Models
{
    public partial class CardCollection
    {
        public int PokemonId { get; set; }
        public int UserId { get; set; }
        public int QuantityNormal { get; set; }
        public int QuantityShiny { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual PokemonCard Pokemon { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual User User { get; set; }
    }
}
