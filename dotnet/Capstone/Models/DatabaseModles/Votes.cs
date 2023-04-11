using Microsoft.Extensions.Configuration.UserSecrets;
using System;

namespace Capstone.Models.DatabaseModles
{
    public class Votes
    {
        public int forumId { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
        public bool isUpVoted { get; set; }
        public bool isDownVoted { get; set; }
        public DateTime createdDate { get; set; }

        public Votes()
        {

        }
        public Votes(int forumId, int postId, int userId, bool isUpVoted, bool isDownVoted, DateTime createDate)
        {
            this.forumId = forumId;
            this.postId = postId;
            this.userId = userId;
            this.isUpVoted = isUpVoted;
            this.isDownVoted = isDownVoted;
            createdDate = createdDate;
        }
    }
}
