using System;
using System.Collections.Generic;

#nullable disable

namespace P2DbContext.Models
{
    public partial class PostComment
    {
        public int CommentId { get; set; }
        public int CommentPostId { get; set; }
        public int CommentUserId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentTimestamp { get; set; }

        public virtual Post CommentPost { get; set; }
        public virtual User CommentUser { get; set; }
    }
}
