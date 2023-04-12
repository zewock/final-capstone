using Capstone.Models.DatabaseModles;
using Capstone.Models.OutgoingDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.DAO
{
    public interface IForumDao
    {
        public int CreateForum(Forum forum);
        public ForumListDTO getAllForums(string userName, string tokenUserRole, int userId = 0);
    }
}
