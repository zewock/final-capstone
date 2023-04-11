namespace Capstone.Models.IncomingDTOs
{
    public class CreatePrivateMessageDTO
    {
        public int OtherUserID { get; set; }

        public string Message { get; set; }

        public CreatePrivateMessageDTO()
        {

        }

        public CreatePrivateMessageDTO(string message, int otherUserID)
        {
            Message = message;
            OtherUserID = otherUserID;
        }
    }
}
