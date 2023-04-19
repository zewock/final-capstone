using System;

namespace Capstone.Models.IntermediaryModles
{
    public class UserData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Userrole { get; set; }
        public DateTime RestoreBanTime { get; set; }

        public UserData()
        {

        }

        public UserData(int userID, string username, string userrole, DateTime restoreBanTime)
        {
            UserID = userID;
            Username = username;
            Userrole = userrole;
            RestoreBanTime = restoreBanTime;
        }


    }
}
