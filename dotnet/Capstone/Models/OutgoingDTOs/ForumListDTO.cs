using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class ForumListDTO
    {        
        public FormsArray[] ForumArray { get; set; }
        
        public bool IsAdmin { get; set; }

        public ForumListDTO()
        {

        }

        public ForumListDTO (List<FormsArray> forumsList, bool isAdmin)
        {
            IsAdmin = isAdmin;
            ForumArray = forumsList.ToArray();
        }
    }
}
