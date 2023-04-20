using Capstone.Models.IntermediaryModles;
using System.Collections.Generic;
using System;

namespace Capstone.Models.OutgoingDTOs
{
    public class TopTenPopularPostsArray
    {
        public int postId { get; set; }
        public string username { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public DateTime createDate { get; set; }
        public int userId { get; set; }
        public int forumId { get; set; }
        public int upVotes { get; set; }
        public int downVotes { get; set; }
        public int upvotesLast24Hours { get; set; }
        public int downvotesLast24Hours { get; set; }

        public TopTenPopularPostsArray ()
        {

        }

        public TopTenPopularPostsArray (int postId, string username, string content, string title, string image, DateTime createDate, int userId, int forumId, int upVotes, int downVotes, int upvotesLast24Hours, int downvotesLast24Hours)
        {
            this.postId = postId;
            this.username = username;
            this.content = content;
            this.title = title;
            this.image = image;
            this.createDate = createDate;
            this.userId = userId;
            this.forumId = forumId;
            this.upVotes = upVotes;
            this.downVotes = downVotes;
            this.upvotesLast24Hours = upvotesLast24Hours;
            this.downvotesLast24Hours = downvotesLast24Hours;
        }
    }
}
