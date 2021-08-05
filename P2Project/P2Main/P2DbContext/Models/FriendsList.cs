using System;
using System.Collections.Generic;

#nullable disable

namespace P2DbContext.Models
{
    public partial class FriendsList
    {
        public int SentRequest { get; set; }
        public int RecievedRequest { get; set; }
        public bool IsPending { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual User RecievedRequestNavigation { get; set; }
        public virtual User SentRequestNavigation { get; set; }
    }
}
