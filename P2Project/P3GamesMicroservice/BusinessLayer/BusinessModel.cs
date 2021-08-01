using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessModel : IBusinessModel
    {
        // method to create and return game object
        public string WhosThatPokemonGame()
        {
            string gameObject = "";
            int numOptions = 4;
            string pictureUrl, correctPokemon;
            string[] options = new string[numOptions];

            // get the pokemon to guess
            dynamic temp = JsonConvert.DeserializeObject(RandomPokemon());
            pictureUrl = temp.sprites.front_default;
            correctPokemon = temp.name;
            // first option is correct name to start
            options[0] = correctPokemon; 

            // get random options
            for (int i = 1; i < numOptions - 1; i++) // numOptions - 1 for last item to be pikachu
            {
                while(options[i] == null)
                {
                    temp = JsonConvert.DeserializeObject(RandomPokemon());
                    if(options.Where(x => x == temp.name.ToString()).FirstOrDefault() == null && temp.name.ToString() != "pikachu") // if name not already an option? and not pikachu because pikachu always option
                        options[i] = temp.name;
                }
            }
            options[numOptions - 1] = "pikachu"; // pikachu always an option for the lolz

            // shuffle options array
            Random rand = new Random();
            options = options.OrderBy(x => rand.Next()).ToArray();

            // make an object to return
            WtpGame wtpGame = new WtpGame(pictureUrl, correctPokemon, options);
            gameObject = JsonConvert.SerializeObject(wtpGame);
            return gameObject;
        }

        public string RandomPokemon()
        {
            var rand = new Random();
            int id = rand.Next(1, 899); // 898 total pokemon?
            string pokemon = "";
            var client = new RestClient("https://pokeapi.co/api/v2/pokemon/"+id);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
            //dynamic myObject = JsonConvert.DeserializeObject(response.Content);
            //if (myObject != null && myObject.Count != 0)
            //{
            //    pokemon = myObject.sprites.front_default;
            //}
        }
    }
}
