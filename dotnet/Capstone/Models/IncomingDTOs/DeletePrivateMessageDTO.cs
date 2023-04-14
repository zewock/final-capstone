namespace Capstone.Models.IncomingDTOs
{
    public class DeletePrivateMessageDTO
    {
        public int MessageID { get; set; }

        public DeletePrivateMessageDTO() 
        { 

        }

        public DeletePrivateMessageDTO(int messageID)
        {
            MessageID = messageID;
        }
    }
}
