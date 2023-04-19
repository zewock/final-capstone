using System;
using System.Data.SqlClient;
using Capstone.Models.IntermediaryModles;
using Capstone.Models.DatabaseModles;
using Capstone.Security;
using Capstone.Security.Models;
using Capstone.Models.IncomingDTOs;
using System.Reflection.PortableExecutable;
using System.Collections.Generic;
using System.Data;

namespace Capstone.DAO
{
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;

        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role, restore_ban_time FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }

        public UserData GetUserData(string username) 
        { 
            UserData userData = new UserData();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select user_id, username, user_role, restore_ban_time from users where username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userData.UserID = Convert.ToInt32(reader["user_id"]);
                        userData.Username = Convert.ToString(reader["username"]);
                        userData.Userrole = Convert.ToString(reader["user_role"]);
                        userData.RestoreBanTime = Convert.ToDateTime(reader["restore_ban_time"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return userData;
        }

        public string GetUsernameById(int userId)
        {
            User returnUser = null;
            string username = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                        username = returnUser.Username;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return username;
        }

        public string GetUserRoleById(int userId)
        {
            string userRole = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userRole = Convert.ToString(reader["user_role"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return userRole;
        }

        public void SetBanTime(BanUserDTO banUserDTO, int bannedUserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update Users " +
                        "Set restore_ban_time = @restore_ban_time " +
                        "where user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", bannedUserID);
                    cmd.Parameters.AddWithValue("@restore_ban_time", banUserDTO.DateBanLifted);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void DeleteUsersContent (int bannedUserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update Forums " +
                        "Set is_visible = 0 " +
                        "where user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", bannedUserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            List<int> userPostsIDs = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select post_id from Forum_Posts where user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", bannedUserID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userPostsIDs.Add(Convert.ToInt32(reader["post_id"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            for (int i = 0; i < userPostsIDs.Count; i++)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("update Forum_Posts " +
                            "Set is_visible = 0 " +
                            "where post_id = @post_id or parent_post_id = @post_id;", conn);
                        cmd.Parameters.AddWithValue("@post_id", userPostsIDs[i]);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
            }
        }

        public int IsUsernameInDatabase (string username)
        {
            int isUsernameInDatabase = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select Count(*) as isUserNameInDataBase from Users where username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isUsernameInDatabase = Convert.ToInt32(reader["isUserNameInDataBase"]);
                }
            }
            return isUsernameInDatabase;
        }

        public void PromoteUserToAdmin (string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Users " +
                    "Set user_role = 'admin' " +
                    "where username = @username;", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
        }

        public void DemoteUserFromAdmin(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Users " +
                    "Set user_role = 'member' " +
                    "where username = @username;", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
        }

        public User AddUser(string username, string password, string role)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (username, password_hash, salt, user_role) VALUES (@username, @password_hash, @salt, @user_role)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", "member");
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(username);
        }

        public int GetUserIDByUsername (string username)
        {
            int UserID = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select user_id from users where username = @username;", conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["user_id"]);
                }
            }
            return UserID;
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["user_role"]),
                RestoreBanDate = Convert.ToDateTime(reader["restore_ban_time"])
            };

            return u;
        }
    }
}
