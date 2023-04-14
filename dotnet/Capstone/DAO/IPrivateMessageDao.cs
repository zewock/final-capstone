using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Capstone.Models.OutgoingDTOs;

namespace Capstone.DAO
{
    public interface IPrivateMessageDao
    {
        public PrivateMessagesDTO GetAllUsersPrivateMessages(int userID);

        public string GetUserRoleFromID(int userID);

        public void CreatePrivateMessage(PrivateMessage privateMessage);

        public PrivateMessage GetPrivateMessage(int messageID);

        public void DeletePrivateMessage(int messageID);
    }
}
