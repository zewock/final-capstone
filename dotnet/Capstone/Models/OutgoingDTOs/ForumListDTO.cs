using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumListDTO
    {        
        public ForumsArray[] ForumArray { get; set; }
        
        public string UserRole { get; set; }

        public ForumListDTO()
        {

        }

        public ForumListDTO (List<ForumsArray> forumsList, string userRole)
        {
            UserRole = userRole;
            ForumArray = forumsList.ToArray();
        }
    }
}
