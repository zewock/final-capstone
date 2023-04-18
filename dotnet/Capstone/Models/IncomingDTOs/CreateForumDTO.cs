using System.Globalization;

namespace Capstone.Models.IncomingDTOs
{
    public class CreateForumDTO
    {
        public string ForumTitle { get; set; }

        public string ForumDescription { get; set; }

        public string ForumTopic { get; set; }

        public string PostHeader { get; set; }

        public string PostContent { get; set; }

        public string PostImageURL { get; set; }

        public CreateForumDTO() 
        { 

        }

        public CreateForumDTO(string forumTitle, string forumDescription, string forumTopic, string postHeader, string postContent, string postImageURL)
        {
            ForumTitle = forumTitle;
            ForumDescription = forumDescription;
            ForumTopic = forumTopic;
            ForumTopic = forumTopic;
            PostHeader = postHeader;
            PostContent = postContent;
            PostImageURL = postImageURL;
        }
    }
}
