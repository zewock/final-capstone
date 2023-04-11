namespace Capstone.Models.DatabaseModles
{
    public class ForumFavorite
    {
        public int userId { get; set; }
        public int forumId { get; set; }

        public ForumFavorite()
        {

        }
        public ForumFavorite(int userId, int forumId)
        {
            this.userId = userId;
            this.forumId = forumId;
        }
    }


}
