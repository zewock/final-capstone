using System.Data;

namespace Capstone.Models.IncomingDTOs
{
    public class ChangeUpvoteDownvoteStateDTO
    {
        public int PostID { get; set; }

        public ChangeUpvoteDownvoteStateDTO ()
        {

        }

        public ChangeUpvoteDownvoteStateDTO(int postID)
        {
            PostID = postID;
        }
    }
}
