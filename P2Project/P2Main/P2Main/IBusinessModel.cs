using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2DbContext.Models;

namespace BusinessLayer
{
    public interface IBusinessModel
    {
        public Dictionary<PokemonCard, bool> rollLootbox(User currentUser); //tested

        public Dictionary<string, bool> buyFromPost(Post post, User currentUser); //tested
        
        public List<Post> getDisplayBoard(); //test

        public Dictionary<CardCollection, PokemonCard> getUserCollection(User currentUser);//tested

        public bool newPost(Post newPost, User currentUser); //test

        public User login(string username, string password); //tested

        public bool signUp(User newUser); //tested

        public bool incrementUserBalance(User currentUser, int coinsToAdd); //tested

        public PokemonCard getPokemonById(int id); //tested

        public User GetUserById(int id); //tested

        public bool RemoveUser(int id); //tested

        public DisplayBoard getPostInfo(int id);

        public Post getPostById(int id);//tested

        public List<RarityType> GetRarityTypes();

        public bool hidePost(int PostID);
    }
}
