namespace Capstone.Models.IntermediaryModles
{
    public class UserPosts
    {
        public int ForumID { get; set; }

        public int PostID { get; set; }

        public UserPosts ()
        {

        }

        public UserPosts (int forumID, int postID)
        {
            ForumID = forumID;
            PostID = postID;
        }
    }
}
