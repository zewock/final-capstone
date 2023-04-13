using Microsoft.VisualBasic;
using System;

namespace Capstone.Models.OutgoingDTOs
{
    public class PrivateMessagesArray
    {
        public int MessageID { get; set; }

        public int FromUserID { get; set; }

        public string FromUsername { get; set; }

        public string FromUserRole { get; set; }

        public int ToUserID { get; set; }

        public string ToUsername { get; set; }

        public string ToUserRole { get; set; }

        public string Message { get; set; }

        public bool IsUserSender { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsOtherUserAdmin { get; set; }

        public bool IsUserAdmin { get; set; }

        public PrivateMessagesArray()
        {

        }

        public PrivateMessagesArray(int messageID, int fromUserID, string fromUsername, string fromUserRole, int toUserID, string toUsername, string toUserRole, string message, bool isUserSender, DateTime createDate, bool isOtherUserAdmin, bool isUserAdmin)
        {
            MessageID = messageID;
            FromUserID = fromUserID;
            FromUsername = fromUsername;
            FromUserRole = fromUserRole;
            ToUserID = toUserID;
            ToUsername = toUsername;
            ToUserRole = toUserRole;
            Message = message;
            IsUserSender = isUserSender;
            CreateDate = createDate;
            IsOtherUserAdmin = isOtherUserAdmin;
            IsUserAdmin = isUserAdmin;
        }
    }
}
