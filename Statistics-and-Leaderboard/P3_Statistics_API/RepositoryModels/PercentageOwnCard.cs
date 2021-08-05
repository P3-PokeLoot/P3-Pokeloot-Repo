using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModels
{
    public class PercentageOwnCard
    {
        public int PokemonId { get; set; }
        public int RarityId { get; set; }
        public string SpriteLink { get; set; }
        public string SpriteLinkShiny { get; set; }
        public string PokemonName { get; set; }
        public double TotalQy { get; set; }
        public double Total_Users { get; set; }
        public double Percentage_OwnCard { get; set; }

    }
}
