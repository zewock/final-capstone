namespace Capstone.Models.DatabaseModles
{
    public class ForumMod
    {
        public int userId { get; set; }
        public int forumId { get; set; }

        public ForumMod()
        {

        }
        public ForumMod(int userId, int forumId)
        {
            this.userId = userId;
            this.forumId = forumId;
        }
    }
}
