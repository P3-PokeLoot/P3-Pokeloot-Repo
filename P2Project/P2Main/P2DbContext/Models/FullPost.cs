using System;

namespace P2DbContext.Models
{
    public class FullPost
    {
        public int PostId { get; set; }
        public int? PokemonId { get; set; }
        public DateTime PostTime { get; set; }
        public string PostDescription { get; set; }
        public int? Price { get; set; }
        public bool StillAvailable { get; set; }
        public bool? IsShiny { get; set; }

        public string UserName { get; set; }
        public int UserId { get; set; }
        public int PostType { get; set; }
        public string PokemonName {get; set;}
        public int RarityId {get; set;}

        public string SpriteLink {get; set;}


    }
}