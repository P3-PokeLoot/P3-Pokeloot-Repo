using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WtpGame
    {
        private string pictureUrl;
        private string correctPokemon;
        private string[] options;

        public string PictureUrl {
            get { return pictureUrl; }
            set { pictureUrl = value; }
        }
        public string CorrectPokemon
        {
            get { return correctPokemon; }
            set { correctPokemon = value; }
        }
        public string[] Options
        {
            get { return options; }
            set { options = value; }
        }

        public WtpGame(string picture, string pokemon, string[] optionsArray)
        {
            pictureUrl = picture;
            correctPokemon = pokemon;
            options = optionsArray;
        }
    }
}
