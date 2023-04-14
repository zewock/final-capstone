using Capstone.Models.DatabaseModles;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        string GetUsernameById(int userId);
        User AddUser(string username, string password, string role);
    }
}
