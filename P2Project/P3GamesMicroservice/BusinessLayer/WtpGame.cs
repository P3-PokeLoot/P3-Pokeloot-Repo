using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class WtpGame
    {
        public string pictureUrl;
        public string correctPokemon;
        public string[] options;

        public WtpGame(string picture, string pokemon, string[] optionsArray)
        {
            pictureUrl = picture;
            correctPokemon = pokemon;
            options = optionsArray;
        }
    }
}
