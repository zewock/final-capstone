namespace Capstone.Models.IncomingDTOs
{
    public class AddRemoveAdmin
    {
        public string Username { get; set; }
        
        public AddRemoveAdmin()
        {

        }

        public AddRemoveAdmin(string username)
        {
            Username = username;
        }
    }
}
