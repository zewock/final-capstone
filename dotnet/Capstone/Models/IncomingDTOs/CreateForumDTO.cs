namespace Capstone.Models.IncomingDTOs
{
    public class CreateForumDTO
    {
        public string Content { get; set; }

        public string Image { get; set; }

        public string Topic { get; set; }

        public string Title { get; set; }

        public CreateForumDTO()
        {

        }

        public CreateForumDTO(string content, string image, string topic, string title)
        {
            Content = content;
            Image = image;
            Topic = topic;
            Title = title;
        }
    }
}
