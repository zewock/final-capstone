namespace Capstone.Models.OutgoingDTOs
{
    public class Forum_ModsArray
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public Forum_ModsArray()
        {

        }

        public Forum_ModsArray(int userID, string username)
        {
            UserID = userID;
            Username = username;
        }
    }
}

