using System.Collections.Generic;

namespace Capstone.Models.OutgoingDTOs
{
    public class PrivateMessagesDTO
    {
        public PrivateMessagesArray[] PrivateMessagesArray { get; set; }

        public bool IsAdmin { get; set; }

        public PrivateMessagesDTO()
        {

        }

        public PrivateMessagesDTO(bool isAdmin, List<PrivateMessagesArray> privateMessageslist)
        {
            IsAdmin = isAdmin;
            PrivateMessagesArray = privateMessageslist.ToArray();
        }
    }
}
