using System.Data;

namespace Capstone.Models.IncomingDTOs
{
    public class ChangeUpvoteDownvoteStateDTO
    {
        public int ForumID { get; set; }

        public int PostID { get; set; }

        public ChangeUpvoteDownvoteStateDTO ()
        {

        }

        public ChangeUpvoteDownvoteStateDTO(int forumID, int postID)
        {
            ForumID = forumID;
            PostID = postID;
        }
    }
}
