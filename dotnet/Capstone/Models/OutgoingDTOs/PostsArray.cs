using Microsoft.VisualBasic;

namespace Capstone.Models.OutgoingDTOs
{
    public class PostsArray
    {
        public int PostID { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public DateAndTime CreateDate { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public int ForumID { get; set; }

        public string Path { get; set; }

        public bool DidUserUpvote { get; set; }

        public bool DidUserDownvote { get;set; }

        public int TotalNumUpvotes { get; set; }

        public int TotalNumDownvotes { get; set; }

        public int UpvotesLast24Hours { get; set; }

        public int DownvotesLast24Hours { get;set; }

        public bool IsAdminPost { get; set; }

        public bool IsOwnerPost { get; set; }

        public bool IsModPost { get; set; }

        public PostsArray(int postID, string content, string image, DateAndTime createDate, 
            int userID, string userName, int forumID, string path, bool didUserUpvote, 
            bool didUserDownvote, int totalNumUpvotes, int totalNumDownvotes, int upvotesLast24Hours, 
            int downvotesLast24Hours, bool isAdminPost, bool isOwnerPost, bool isModPost)
        {
            PostID = postID;
            Content = content;
            Image = image;
            CreateDate = createDate;
            UserID = userID;
            UserName = userName;
            ForumID = forumID;
            Path = path;
            DidUserUpvote = didUserUpvote;
            DidUserDownvote = didUserDownvote;
            TotalNumUpvotes = totalNumUpvotes;
            TotalNumDownvotes = totalNumDownvotes;
            UpvotesLast24Hours = upvotesLast24Hours;
            DownvotesLast24Hours = downvotesLast24Hours;
            IsAdminPost = isAdminPost;
            IsOwnerPost = isOwnerPost;
            IsModPost = isModPost;
        }
    }
}
