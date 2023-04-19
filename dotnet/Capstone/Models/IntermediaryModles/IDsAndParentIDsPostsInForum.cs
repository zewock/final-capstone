namespace Capstone.Models.IntermediaryModles
{
    public class IDsAndParentIDsPostsInForum
    {
        public int PostID { get; set; }
        public int ParentPostID { get; set; }

        public IDsAndParentIDsPostsInForum() { }
        public IDsAndParentIDsPostsInForum(int postID, int parentPostID) 
        { 
            PostID = postID;
            ParentPostID = parentPostID;
        }
    }
}
