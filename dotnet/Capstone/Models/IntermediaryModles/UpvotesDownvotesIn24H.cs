namespace Capstone.Models.IntermediaryModles
{
    public class UpvotesDownvotesInLast24H
    {
        public int upvotesInLast24H { get; set; }
        public int  downvotesInLast24H { get; set; }

        public int post_id { get; set; }
        public int forum_id { get; set; }

        public UpvotesDownvotesInLast24H()
        {

        }

        public UpvotesDownvotesInLast24H(int upvotesInLast24H, int downvotesInLast24H, int post_id, int forum_id)
        {
            this.upvotesInLast24H = upvotesInLast24H;
            this.downvotesInLast24H = downvotesInLast24H;
            this.post_id = post_id;
            this.forum_id = forum_id;
        }
 
    }
}
