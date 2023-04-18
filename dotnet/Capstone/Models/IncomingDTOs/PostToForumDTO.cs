namespace Capstone.Models.IncomingDTOs
{
    public class PostToForumDTO
    {
        public int ForumID { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public int? ParentPostID { get; set; }

        public PostToForumDTO() 
        {
            
        }
        
        public PostToForumDTO(int forumID, string header, string content, string image, int? parentPostID )
        {
            ForumID = forumID;
            Header = header;
            Content = content;
            Image = image;
            ParentPostID = parentPostID;
        }
    }
}
