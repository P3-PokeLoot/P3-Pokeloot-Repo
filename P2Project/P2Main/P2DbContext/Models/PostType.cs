using System;
using System.Collections.Generic;

#nullable disable

namespace P2DbContext.Models
{
    public partial class PostType
    {
        public PostType()
        {
            DisplayBoards = new HashSet<DisplayBoard>();
        }

        public int PostType1 { get; set; }
        public string PostCategory { get; set; }

        public virtual ICollection<DisplayBoard> DisplayBoards { get; set; }
    }
}
