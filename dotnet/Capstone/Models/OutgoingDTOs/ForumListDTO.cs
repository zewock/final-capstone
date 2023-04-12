using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumListDTO
    {        
        public FormsArray[] ForumArray { get; set; }
        
        public string userRole { get; set; }

        public ForumListDTO()
        {

        }

        public ForumListDTO (List<FormsArray> forumsList, string isAdmin)
        {
            userRole = isAdmin;
            ForumArray = forumsList.ToArray();
        }
    }
}
