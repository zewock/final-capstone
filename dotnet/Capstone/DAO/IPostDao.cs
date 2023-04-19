using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Capstone.Models.IntermediaryModles;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        public List<ForumPostWithVotesAndUserName> GetPostsByForumId(int forumId);
        public List<ForumPostWithVotesAndUserName> SearchPostsForKeyword(string keyword);
        public List<ForumPostWithVotesAndUserName> GetCompletePostThreadById(int postId);
        public void PostToForum(PostToForumDTO postToForumDTO, int userID);
        public int GetUserIDByPostID(int postID);
        public List<int> GetModsIDsByForumID(int forumPostID);
        public void DeletePost(int postID);
        public int IsUserOwnerOfForum(int userID, int forumID);
        public int IsUserModOfForum(string username, int forumID);
        public void AddMod(int userID, int forumID);
        public void RemoveMod(int userID, int forumID);
        public int IsForumFavorited(int userID, int forumID);
        public void RemoveFavorite(int userID, int forumID);
        public void AddFavoriteForum(int userID, int forumID);
        public bool DoseForumExist(int forumID);
        public IsUpvotedDownVoted GetPostsUpvotesDownvotes(int userID, int postID, IsUpvotedDownVoted isUpvotedDownVoted);
        public bool DosePostUpvoteDownExist(int postID, int forumID);
        public bool DosePostExist(int postID, int forumID);
        public void CreateUpvoteDownvote(PostsUpvotesDownvotes postsUpvotesDownvotes);
        public void DeleteUpvoteDownvote(int userID, int postID);
        public void UpdateUpvoteDownvote(PostsUpvotesDownvotes postsUpvotesDownvotes);
        public int GetForumOwnerUserID(int forumID);
        public List<IDsAndParentIDsPostsInForum> GetIDsAndParentIDsInForm(int forumID, int postID);

    }
}
