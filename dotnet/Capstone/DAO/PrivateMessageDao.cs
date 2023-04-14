using Capstone.Models;
using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System;
using System.Data.SqlClient;
using Capstone.Models.DatabaseModles;
using Capstone.Models.OutgoingDTOs;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;


namespace Capstone.DAO
{
    public class PrivateMessageDao : IPrivateMessageDao
    {
        private readonly string connectionString;
        public PrivateMessageDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public PrivateMessagesDTO GetPrivateMessages(int userID)
        {
            PrivateMessagesDTO privateMessagesDTO = new PrivateMessagesDTO();
            List<PrivateMessagesArray> privateMessagesArray = new List<PrivateMessagesArray>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select message_id, " +
                    "from_user, fu.username as from_username, fu.user_role as from_user_role, " +
                    "to_user, tu.username as to_username, tu.user_role as to_user_role, " +
                    "message, pm.create_date as create_date, is_visable, " +
                    "(case " +
                    "when fu.user_id = @user_id then cast(1 as bit) " +
                    "else cast(0 as bit) " +
                    "end) as is_user_sender, " +
                    "(case " +
                    "when (tu.user_role like 'admin' and fu.user_id = @user_id) " +
                    "or (fu.user_role like 'admin' and tu.user_id = @user_id) then cast(1 as bit) " +
                    "else cast(0 as bit) " +
                    "end) as is_other_user_admin, " +
                    "(case " +
                    "when (fu.user_role like 'admin' and tu.user_id != @user_id) " +
                    "or (tu.user_role like 'admin' and fu.user_id != @user_id) then cast(1 as bit) " +
                    "else cast(0 as bit) " +
                    "end) as is_user_admin " +
                    "from Private_Messages as pm " +
                    "join users as fu on pm.from_user = fu.user_id " +
                    "join users as tu on pm.to_user = tu.user_id " +
                    "where (fu.user_id = @user_id OR tu.user_id = @user_id) AND is_visable = 1 " +
                    "order by message_id", conn);
                cmd.Parameters.AddWithValue("@user_id", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PrivateMessagesArray privateMessage = new PrivateMessagesArray();
                    privateMessage.MessageID = Convert.ToInt32(reader["message_id"]);
                    privateMessage.FromUserID = Convert.ToInt32(reader["from_user"]);
                    privateMessage.FromUsername = Convert.ToString(reader["from_username"]);
                    privateMessage.FromUserRole = Convert.ToString(reader["from_user_role"]);
                    privateMessage.ToUserID = Convert.ToInt32(reader["to_user"]);
                    privateMessage.ToUsername = Convert.ToString(reader["to_username"]);
                    privateMessage.ToUserRole = Convert.ToString(reader["to_user_role"]);
                    privateMessage.Message = Convert.ToString(reader["message"]);
                    privateMessage.CreateDate = Convert.ToDateTime(reader["create_date"]);
                    privateMessage.IsUserSender = Convert.ToBoolean(reader["is_user_sender"]);
                    privateMessage.IsOtherUserAdmin = Convert.ToBoolean(reader["is_other_user_admin"]);
                    privateMessage.IsUserAdmin = Convert.ToBoolean(reader["is_user_admin"]);
                    privateMessagesArray.Add(privateMessage);
                }
                privateMessagesDTO.PrivateMessagesArray = privateMessagesArray.ToArray();
            }
                return privateMessagesDTO;
        }

        public string GetUserRoleFromID(int userID)
        {
            string userRole = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select user_role from Users where user_id = @user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userRole = Convert.ToString(reader["user_role"]);
                }
            }
            return userRole;
        }
        /*
        public Forum getForumById(int userId)
        {   Forum forum = new Forum();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

            }
        }
        

        //GetAllForums() - Retrieves all forums.
        public List<Forum> getAllForums()
        {

        }
        //GetFavoritedForumsByUserId(int userId) - Retrieves favorited forums for a specific user by ID.
        public List<Forum> getFavoritedForumsByUserId(int userID)
        {

        }
        //CreateForum(Forum forum) - Creates a new forum.
        public ActionResult<Forum> createForum()
        {

        }
        //UpdateForum(Forum forum) - Updates an existing forum.
        public ActionResult<Forum> updateForum(int forumId)
        {

        }
        //DeleteForum(int forumId) - Deletes a forum by ID.
        public ActionResult deleteForum(int forumId)
        {

        }
        */

        public Forum GetTransferFromReader(SqlDataReader reader)
        {
            Forum forum = new Forum();

            forum.forumId = Convert.ToInt32(reader["forum_id"]);
            forum.topic = Convert.ToString(reader["topic"]);
            forum.ownerId = Convert.ToInt32(reader["owner_id"]);
            forum.createdDate = Convert.ToDateTime(reader["create_date"]);
            forum.isVisible = Convert.ToBoolean(reader["isVisible"]);
            return forum;

        }
    }
}