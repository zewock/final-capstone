using Capstone.Models.IntermediaryModles;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        public ActionResult<List<ForumPostWithVotesAndUserName>> GetPostsByForumId(int forumId);
        public ActionResult<List<ForumPostWithVotesAndUserName>> SearchPostsForKeyword(string keyword);
    
    }
}
