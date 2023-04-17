using System;

namespace Capstone.Models.DatabaseModles
{
    public class ForumPost
    {
        public int PostID { get; set; }
        public string Header { get; set; }
        public string PostContent { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserID { get; set; }
        public int ForumID { get; set; }
        public bool IsVisable { get; set; }
        public int? ParentPostID { get; set; }

        public ForumPost ()
        {

        }

        public ForumPost(int postID, string header, string postConetent, string imageURL,
            DateTime createDate, int userID, int forumID, bool isVisable, int? parentPostID)
        {
            PostID = postID;
            Header = header;
            PostContent = postConetent;
            ImageURL = imageURL;
            CreateDate= createDate;
            UserID = userID;
            ForumID = forumID;
            IsVisable = isVisable;
            ParentPostID = parentPostID;
        }
    }
}
