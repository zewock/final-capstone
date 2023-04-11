namespace Capstone.Models.IncomingDTOs
{
    public class DeleteMessageDTO
    {
        public int MessageID { get; set; }

        public DeleteMessageDTO() 
        { 

        }

        public DeleteMessageDTO(int messageID)
        {
            MessageID = messageID;
        }
    }
}
