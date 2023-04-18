namespace Capstone.Models.IncomingDTOs
{
    public class AddRemoveModDTO
    {
        public int formID { get; set; }
        
        public string username { get; set; }

        public AddRemoveModDTO()
        {

        }

        public AddRemoveModDTO(int formID, string username)
        {
            this.formID = formID;
            this.username = username;
        }
    }
}
