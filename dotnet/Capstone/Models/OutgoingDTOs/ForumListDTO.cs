using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumListDTO
    {        
        public FormsArray[] ForumArray { get; set; }
        
        public string UserRole { get; set; }

        public ForumListDTO()
        {

        }

        public ForumListDTO (List<FormsArray> forumsList, string userRole)
        {
            UserRole = userRole;
            ForumArray = forumsList.ToArray();
        }
    }
}
