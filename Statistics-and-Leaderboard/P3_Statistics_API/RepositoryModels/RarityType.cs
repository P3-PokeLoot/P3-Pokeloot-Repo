using System;
using System.Collections.Generic;

#nullable disable

namespace RepositoryModels
{
    public partial class RarityType
    {
        public RarityType()
        {
            PokemonCards = new HashSet<PokemonCard>();
        }

        public int RarityId { get; set; }
        public string RarityCategory { get; set; }

        public virtual ICollection<PokemonCard> PokemonCards { get; set; }
    }
}
