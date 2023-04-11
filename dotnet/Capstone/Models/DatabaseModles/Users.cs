using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Capstone.Models.DatabaseModles
{
    public class Users
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public bool isAdmin { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastLoginDate { get; set; }
        public string password { get; set; }
        public DateTime restoreBanTime { get; set; }
        public int[] favoriteForums { get; set; }

        public Users()
        {

        }
        public Users(int userId, string userName, bool isAdmin, DateTime createdDate, DateTime lastLoginDate, string password, DateTime restoreBanTime, int[] favoriteForums)
        {
            this.userId = userId;
            this.userName = userName;
            this.isAdmin = isAdmin;
            this.createdDate = createdDate;
            this.lastLoginDate = lastLoginDate;
            this.password = password;
            this.restoreBanTime = restoreBanTime;
            this.favoriteForums = favoriteForums;
        }
    }
}
