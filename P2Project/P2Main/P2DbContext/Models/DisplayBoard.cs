using System;
using System.Collections.Generic;

#nullable disable

namespace P2DbContext.Models
{
    public partial class DisplayBoard
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int PostType { get; set; }

        public virtual Post Post { get; set; }
        public virtual PostType PostTypeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
