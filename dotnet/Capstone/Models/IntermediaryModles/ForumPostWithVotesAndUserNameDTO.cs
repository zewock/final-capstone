using System;
using System.Collections.Generic;

namespace Capstone.Models.IntermediaryModles
{
    public class ForumPostWithVotesAndUserName

    {
        public long? parentPostId { get; set; }
        public int postId { get; set; }
        public string username { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public DateTime createDate { get; set; }
        public int userId { get; set; }
        public string authorUserName { get; set; }
        public int forumId { get; set; }
        public int upVotes { get; set; }
        public int downVotes { get; set; }
        public int upvotesLast24Hours { get; set; }
        public int downvotesLast24Hours { get; set; }
        public bool isVisible { get; set; }
        public int depth { get; set; }

        public List<ForumPostWithVotesAndUserName> replies { get; set; }

        public ForumPostWithVotesAndUserName()
        {
            replies = new List<ForumPostWithVotesAndUserName>();
        }
        public ForumPostWithVotesAndUserName(long? parentPostId, int postId, string content, string title, string image, DateTime createDate, int userId, string authorUserName, int forumId, int upVotes, int downVotes, bool isVisible, int depth)
        {
            this.parentPostId = parentPostId;
            this.postId = postId;
            this.content = content;
            this.title = title;
            this.image = image;
            this.createDate = createDate;
            this.userId = userId;
            this.authorUserName = authorUserName;
            this.forumId = forumId;
            this.upVotes = upVotes;
            this.downVotes = downVotes;
            this.isVisible = isVisible;
            this.depth = depth;
        }
    }
}
