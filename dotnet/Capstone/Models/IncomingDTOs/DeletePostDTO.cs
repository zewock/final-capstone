namespace Capstone.Models.IncomingDTOs
{
    public class DeletePostDTO
    {
        public int FormID { get; set; }

        public int PostID { get; set; }

        public DeletePostDTO()
        {

        }

        public DeletePostDTO(int formID, int postID)
        {
            FormID = formID;
            PostID = postID;
        }
    }
}
