using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryModels
{
    public partial class CardCollection
    {
        public int PokemonId { get; set; }
        public int UserId { get; set; }
        public int QuantityNormal { get; set; }
        public int QuantityShiny { get; set; }

        public virtual PokemonCard Pokemon { get; set; }
        public virtual User User { get; set; }
    }
}
