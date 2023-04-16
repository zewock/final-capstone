using Microsoft.Extensions.Configuration.UserSecrets;
using System;

namespace Capstone.Models.DatabaseModles
{
    public class postsUpvotesDownvotes
    {
        public int forumId { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        public bool isUpVoted { get; set; }
        public bool isDownVoted { get; set; }
        public DateTime createDate { get; set; }

        public postsUpvotesDownvotes()
        {

        }
        public postsUpvotesDownvotes(int forumId, int postId, int userId, bool isUpVoted, bool isDownVoted, DateTime createDate)
        {
            this.forumId = forumId;
            this.postId = postId;
            this.userId = userId;
            this.isUpVoted = isUpVoted;
            this.isDownVoted = isDownVoted;
            this.createDate = createDate;
        }
    }
}
