namespace Capstone.Models.IntermediaryModles
{
    public class IsUpvotedDownVoted
    {
        public int postID { get; set; }
        public bool isUpvoted { get; set; }
        public bool isDownvoted { get; set; }

        public  IsUpvotedDownVoted() 
        {
            
        }
        public IsUpvotedDownVoted(int postID, bool isUpvoted, bool isDownvoted)
        {
            this.postID = postID;
            this.isUpvoted = isUpvoted;
            this.isDownvoted = isDownvoted;
        }
    }
}
