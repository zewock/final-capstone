namespace Capstone.Models.IntermediaryModles
{
    public class ForumFavoriteAndUsername
    {
        public int userId { get; set; }
        public int forumId { get; set; }

        public string username { get; set; }

        public ForumFavoriteAndUsername()
        {

        }
        public ForumFavoriteAndUsername(int userId, int forumId, string username)
        {
            this.userId = userId;
            this.forumId = forumId;
            this.username = username;
        }
    }
}