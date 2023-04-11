using System;

namespace Capstone.Models.DatabaseModles
{
    public class ForumPost
    {
        public int postId { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public DateTime createDate { get; set; }
        public int userId { get; set; }
        public int forumId { get; set; }
        public bool isVisible { get; set; }
        public string path { get; set; }

        public ForumPost()
        {

        }
        public ForumPost(int postId, string content, string image, DateTime createDate, int userId, int forumId, bool isVisible, string path)
        {
            this.postId = postId;
            this.content = content;
            this.image = image;
            this.createDate = createDate;
            this.userId = userId;
            this.forumId = forumId;
            this.isVisible = isVisible;
            this.path = path;
        }
    }
}
