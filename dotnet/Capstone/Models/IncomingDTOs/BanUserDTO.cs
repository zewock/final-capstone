using Microsoft.VisualBasic;
using System;

namespace Capstone.Models.IncomingDTOs
{
    public class BanUserDTO
    {
        public int UserID { get; set; }

        public DateTime DateBanLifted { get; set; }

        public bool DeleteAllTraffic { get; set; }

        public BanUserDTO()
        {

        }

        public BanUserDTO(int userID, DateTime dateBanLifted, bool deleteAllTraffic)
        {
            UserID = userID;
            DateBanLifted = dateBanLifted;
            DeleteAllTraffic = deleteAllTraffic;
        }
    }
}
