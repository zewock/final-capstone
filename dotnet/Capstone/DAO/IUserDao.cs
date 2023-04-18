using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        string GetUsernameById(int userId);
        User AddUser(string username, string password, string role);
        public string GetUserRoleById(int userId);
        public void SetBanTime(BanUserDTO banUserDTO);
        public void DeleteUsersContent(BanUserDTO banUserDTO);
        public int IsUsernameInDatabase(string username);
        public void PromoteUserToAdmin(string username);
        public void DemoteUserFromAdmin(string username);
        public int GetUserIDByUsername(string username);
    }
}
