using Capstone.Models.IncomingDTOs;
using Capstone.Models.OutgoingDTOs;

namespace Capstone.DAO
{
    public interface IPrivateMessageDao
    {
        public PrivateMessagesDTO GetPrivateMessages(int userID);

        public string GetUserRoleFromID(int userID);

        public void CreatePrivateMessage(CreatePrivateMessageDTO CreatePrivateMessageDTO, int senderUserID);
    }
}
