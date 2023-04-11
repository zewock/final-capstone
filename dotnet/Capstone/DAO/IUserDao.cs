using Capstone.Models.DatabaseModles;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);
    }
}
