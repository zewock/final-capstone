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


    }
}
