using System;

namespace Capstone.Models.DatabaseModles
{
    public class PrivateMessage
    {  // Model to return specific private message
        public int messageId { get; set; }
        public int fromUserId { get; set; }
        public int toUserId { get; set; }
        public string message { get; set; }
        public DateTime createdDate { get; set; }
        public bool isVisible { get; set; }

        public PrivateMessage()
        {

        }
        public PrivateMessage(int messageId, int fromUserId, int toUserId, string message, DateTime createdDate, bool isVisible)
        {
            this.messageId = messageId;
            this.fromUserId = fromUserId;
            this.toUserId = toUserId;
            this.message = message;
            this.createdDate = createdDate;
            this.isVisible = isVisible;
        }
    }

}
