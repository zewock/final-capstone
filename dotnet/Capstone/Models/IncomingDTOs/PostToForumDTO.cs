namespace Capstone.Models.IncomingDTOs
{
    public class PostToForumDTO
    {
        public int ForumID { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public string Path { get; set; }

        public PostToForumDTO() 
        {

        }

        public PostToForumDTO(int forumID, string content, string image, string path)
        {
            ForumID = forumID;
            Content = content;
            Image = image;
            Path = path;
        }
    }
}
