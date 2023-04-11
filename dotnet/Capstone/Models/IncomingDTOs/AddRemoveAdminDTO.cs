namespace Capstone.Models.IncomingDTOs
{
    public class AddRemoveAdminDTO
    {
        public string Username { get; set; }
        
        public AddRemoveAdminDTO()
        {

        }

        public AddRemoveAdminDTO(string username)
        {
            Username = username;
        }
    }
}
