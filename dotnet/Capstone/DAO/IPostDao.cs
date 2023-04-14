using Capstone.Models.IntermediaryModles;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        public List<ForumPostWithVotesAndUserName> GetPostsByForumId(int forumId);
    }
}
