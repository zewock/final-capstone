using Microsoft.VisualBasic;
using System;

namespace Capstone.Models.IncomingDTOs
{
    public class BanUserDTO
    {
        public string Username { get; set; }

        public DateTime DateBanLifted { get; set; }

        public bool DeleteAllTraffic { get; set; }

        public BanUserDTO()
        {

        }

        public BanUserDTO(string username, DateTime dateBanLifted, bool deleteAllTraffic)
        {
            Username = username;
            DateBanLifted = dateBanLifted;
            DeleteAllTraffic = deleteAllTraffic;
        }
    }
}
