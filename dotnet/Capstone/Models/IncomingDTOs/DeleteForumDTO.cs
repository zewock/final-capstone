namespace Capstone.Models.IncomingDTOs
{
    public class DeleteForumDTO
    {
        public int ForumId { get; set; }

        public DeleteForumDTO() 
        { 

        }

        public DeleteForumDTO(int forumId)
        {
            ForumId = forumId;
        }
    }
}
