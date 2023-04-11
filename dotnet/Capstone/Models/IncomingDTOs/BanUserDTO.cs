using Microsoft.VisualBasic;

namespace Capstone.Models.IncomingDTOs
{
    public class BanUserDTO
    {
        public int Username { get; set; }

        public DateAndTime DateBanLifted { get; set; }

        public bool DeleteAllTraffic { get; set; }

        public BanUserDTO()
        {

        }

        public BanUserDTO(int username, DateAndTime dateBanLifted, bool deleteAllTraffic)
        {
            Username = username;
            DateBanLifted = dateBanLifted;
            DeleteAllTraffic = deleteAllTraffic;
        }
    }
}
