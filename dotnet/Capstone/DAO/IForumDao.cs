using Capstone.Models.DatabaseModles;
using Capstone.Models.OutgoingDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.DAO
{
    public interface IForumDao
    {
        public int CreateForum(Forum forum);
        public ActionResult<ForumListDTO> getAllForums(int userId = 0, string userName);
    }
}
