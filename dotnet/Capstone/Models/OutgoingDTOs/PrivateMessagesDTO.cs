using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class PrivateMessagesDTO
    {
        public PrivateMessagesArray[] PrivateMessagesArray { get; set; }

        public string UserRole { get; set; }

        public PrivateMessagesDTO()
        {

        }

        public PrivateMessagesDTO(string userRole, List<PrivateMessagesArray> privateMessageslist)
        {
            UserRole = userRole;
            PrivateMessagesArray = privateMessageslist.ToArray();
        }
    }
}
