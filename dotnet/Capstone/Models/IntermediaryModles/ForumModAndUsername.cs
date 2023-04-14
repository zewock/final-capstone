namespace Capstone.Models.IntermediaryModles
{
    public class ForumModAndUsername
    {
        public int userId { get; set; }
        public int forumId { get; set; }

        public string username { get; set; }

        public ForumModAndUsername()
        {

        }
        public ForumModAndUsername(int userId, int forumId, string username)
        {
            this.userId = userId;
            this.forumId = forumId;
            this.username = username;
        }
    }
}