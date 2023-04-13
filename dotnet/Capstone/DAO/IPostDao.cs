using Capstone.Models.DatabaseModles;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        public List<ForumPost> GetPostsByForumId(int forumId);
    }
}
