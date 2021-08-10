using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2DbContext.Models
{
    public class FullFriend
    {

        public string FriendName { get; set; }

        public int FriendId { get; set; }
        public int FriendLevel { get; set; }
        public int TotalCards { get; set; }
        public bool IsPending { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
