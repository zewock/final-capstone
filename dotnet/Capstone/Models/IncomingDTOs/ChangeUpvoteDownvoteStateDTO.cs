using System.Data;

namespace Capstone.Models.IncomingDTOs
{
    public class ChangeUpvoteDownvoteStateDTO
    {
        public int PostID { get; set; }

        public int ForumID { get; set; }

        public ChangeUpvoteDownvoteStateDTO ()
        {

        }

        public ChangeUpvoteDownvoteStateDTO(int postID, int forumID)
        {
            PostID = postID;
            ForumID = forumID;
        }
    }
}
