using Capstone.Models.OutgoingDTOs;

namespace Capstone.DAO
{
    public interface IPrivateMessageDao
    {
        public PrivateMessagesDTO GetPrivateMessages(int userID);
    }
}
