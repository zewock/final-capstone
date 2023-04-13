
using Capstone.Models;
using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System;
using System.Data.SqlClient;
using Capstone.Models.DatabaseModles;
using System.Collections.Generic;
using Capstone.Models.OutgoingDTOs;
using System.Linq;

namespace Capstone.DAO
{
    public class ForumDao : IForumDao
    {
        private readonly string connectionString;
        public ForumDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int CreateForum(Forum forum)
        {
            int newForumID;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "insert into Forums (topic, title, user_id) " +
                                "OUTPUT INSERTED.forum_id " +
                                "values (@topic, @title, @user_id);";

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@topic", forum.topic);
                cmd.Parameters.AddWithValue("@title", forum.title);
                cmd.Parameters.AddWithValue("@user_id", forum.ownerId);

                newForumID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return newForumID;
        }




        /*public Forum getForumById(int userId)
        {   Forum forum = new Forum();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {

            }
        }*/


        // GetAllForums() - Retrieves all forums.
        public ForumListDTO getAllForums(string userName, string tokenUserRole, int userId)
        {
            List<ForumsArray> forumsList = new List<ForumsArray>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT forums.forum_id, forums.topic, forums.user_id, forums.create_date, users.username, " +
                                                "(CASE WHEN forum_favorites.user_id = forums.user_id THEN 1 ELSE 0 END) AS is_favorite, " +
                                                "MAX(CASE WHEN Forum_Mods.user_id = @user_id THEN 1 ELSE 0 END) AS is_moderator, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.create_date > DATEADD(day, -1, GETDATE()) AND p.is_upvoted = 1) AS upvoteswithin24hours, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.create_date > DATEADD(day, -1, GETDATE()) AND p.is_downvoted = 1) AS downvoteswithin24hours, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.is_upvoted = 1) AS totalupvotes, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.is_downvoted = 1) AS totaldownvotes FROM forums " +
                                                "LEFT JOIN forum_mods ON forums.forum_id = forum_mods.forum_id AND forum_mods.user_id = @user_id " +
                                                "LEFT JOIN users ON Forums.user_id = users.user_id " +
                                                "LEFT JOIN forum_favorites ON forums.forum_id = forum_favorites.forum_id AND forum_favorites.user_id = @user_id " +
                                                "WHERE forums.is_visible = 1 OR forums.user_id = @user_id " +
                                                "GROUP BY forums.forum_id, forums.user_id, forums.topic, forums.create_date, forums.is_visible, users.username, forum_favorites.user_id;", conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ForumsArray forumsArray = new ForumsArray();
                    forumsArray.ForumID = Convert.ToInt32(reader["forum_id"]);
                    forumsArray.Topic = Convert.ToString(reader["topic"]);
                    forumsArray.CreateDate = Convert.ToDateTime(reader["create_date"]);
                    forumsArray.OwnerID = Convert.ToInt32(reader["user_id"]);
                    forumsArray.OwnerUsername = Convert.ToString(reader["username"]);
                    forumsArray.TotalNumUpvotes = Convert.ToInt32(reader["totalupvotes"]);
                    forumsArray.TotalNumDownvotes = Convert.ToInt32(reader["totaldownvotes"]);
                    forumsArray.UpvotesLast24Hours = Convert.ToInt32(reader["upvoteswithin24hours"]);
                    forumsArray.DownvotesLast24Hours = Convert.ToInt32(reader["downvoteswithin24hours"]);
                    forumsArray.IsModerator = Convert.ToBoolean(reader["is_moderator"]);
                    forumsArray.IsFavoriteForum = Convert.ToBoolean(reader["is_favorite"]);
                    forumsList.Add(forumsArray);
                }
            }
            ForumListDTO forumListDto = new ForumListDTO(forumsList, tokenUserRole);
            return forumListDto;
        }

       /* //GetFavoritedForumsByUserId(int userId) - Retrieves favorited forums for a specific user by ID.
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

        }*/
        

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

        
    