using System;

namespace Capstone.Models.DatabaseModles
{
    public class ForumPost
    {
        int PostID { get; set; }
        string Header { get; set; }
        string PostContent { get; set; }
        string ImageURL { get; set; }
        DateTime CreateDate { get; set; }
        int UserID { get; set; }
        int ForumID { get; set; }
        bool IsVisable { get; set; }
        int ParentPostID { get; set; }

        public ForumPost ()
        {

        }

        public ForumPost(int postID, string header, string postConetent, string imageURL,
            DateTime createDate, int userID, int forumID, bool isVisable, int parentPostID)
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
