namespace Capstone.Models.OutgoingDTOs
{
    public class Forums_FavoritesArray
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public Forums_FavoritesArray()
        { 
        
        }
            
        public Forums_FavoritesArray(int userID, string username)
        {
            UserID = userID;
            Username = username;
        }


    }
}
