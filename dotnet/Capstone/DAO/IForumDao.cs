using Capstone.Models.DatabaseModles;
using Capstone.Models.IntermediaryModles;
using Capstone.Models.OutgoingDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IForumDao
    {
        public int CreateForum(Forum forum);
        public ForumListDTO getAllForums(string userName, string tokenUserRole, int userId);
        public List<ForumModAndUsername> GetAllForumMods();
        public List<ForumFavoriteAndUsername> GetAllForumFavorites();


    }

}
