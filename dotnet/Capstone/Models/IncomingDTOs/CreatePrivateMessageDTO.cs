namespace Capstone.Models.IncomingDTOs
{
    public class CreatePrivateMessageDTO
    {
        public string OtherUserUsername { get; set; }

        public string Message { get; set; }

        public CreatePrivateMessageDTO()
        {

        }

        public CreatePrivateMessageDTO(string message, string otherUserUsername)
        {
            Message = message;
            OtherUserUsername= otherUserUsername;
        }
    }
}
