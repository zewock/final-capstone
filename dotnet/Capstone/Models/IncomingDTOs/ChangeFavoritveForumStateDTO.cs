namespace Capstone.Models.IncomingDTOs
{
    public class ChangeFavoritveForumStateDTO
    {
        public int ForumId { get; set; }

        public ChangeFavoritveForumStateDTO()
        {

        }

        public ChangeFavoritveForumStateDTO(int forumId)
        {
            ForumId = forumId;
        }
    }
}
