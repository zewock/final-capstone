using Microsoft.VisualBasic;

namespace Capstone.Models.OutgoingDTOs
{
    public class PrivateMessagesArray
    {
        public int MessageID { get; set; }

        public int OtherUserID { get; set; }

        public string OtherUsername { get; set; }

        public string Message { get; set; }

        public bool IsUserSender { get; set; }

        public DateAndTime CreateDate { get; set; }

        public PrivateMessagesArray()
        {

        }

        public PrivateMessagesArray(int messageID, int otherUserID, string otherUsername,
            string message, bool isUserSender, DateAndTime createDate)
        {
            MessageID = messageID;
            OtherUserID = otherUserID;
            OtherUsername = otherUsername;
            Message = message;
            IsUserSender = isUserSender;
            CreateDate = createDate;
        }
    }
}
