using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Capstone.Models.IntermediaryModles;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        string GetUsernameById(int userId);
        User AddUser(string username, string password, string role);
        public string GetUserRoleById(int userId);
        public void SetBanTime(BanUserDTO banUserDTO, int bannedUserID);
        public void DeleteUsersContent(int bannedUserID);
        public int IsUsernameInDatabase(string username);
        public void PromoteUserToAdmin(string username);
        public void DemoteUserFromAdmin(string username);
        public int GetUserIDByUsername(string username);
        public UserData GetUserData(string username);
    }
}
