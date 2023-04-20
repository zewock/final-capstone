
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
using Capstone.Models.IntermediaryModles;

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

                string sql = "insert into Forums (topic, title, user_id, description) " +
                                "OUTPUT INSERTED.forum_id " +
                                "values (@topic, @title, @user_id, @description);";

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@topic", forum.topic);
                cmd.Parameters.AddWithValue("@title", forum.title);
                cmd.Parameters.AddWithValue("@user_id", forum.ownerId);
                cmd.Parameters.AddWithValue("@description", forum.description);

                newForumID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return newForumID;
        }

        public ForumListDTO getAllForums(string userName, string tokenUserRole, int userId)
        {
            List<ForumsArray> forumsList = new List<ForumsArray>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT forums.forum_id, forums.topic, forums.user_id, forums.create_date, users.username, forums.title, forums.description, forums.most_recent_post_date, " +
                                                "(CASE WHEN forum_favorites.user_id = forums.user_id THEN 1 ELSE 0 END) AS is_favorite, " +
                                                "MAX(CASE WHEN Forum_Mods.user_id = @user_id THEN 1 ELSE 0 END) AS is_moderator, " +
                                                "MAX(CASE WHEN Forums.user_id = 1 THEN 1 ELSE 0 END) AS is_owner, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.create_date > DATEADD(day, -1, GETDATE()) AND p.is_upvoted = 1) AS upvoteswithin24hours, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.create_date > DATEADD(day, -1, GETDATE()) AND p.is_downvoted = 1) AS downvoteswithin24hours, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.is_upvoted = 1) AS totalupvotes, " +
                                                "(SELECT COUNT(*) FROM Post_Upvotes_Downvotes p WHERE p.forum_id = forums.forum_id AND p.is_downvoted = 1) AS totaldownvotes FROM forums " +
                                                "LEFT JOIN forum_mods ON forums.forum_id = forum_mods.forum_id AND forum_mods.user_id = @user_id " +
                                                "LEFT JOIN users ON Forums.user_id = users.user_id " +
                                                "LEFT JOIN forum_favorites ON forums.forum_id = forum_favorites.forum_id AND forum_favorites.user_id = @user_id " +
                                                "WHERE forums.is_visible = 1 " +
                                                "GROUP BY forums.forum_id, forums.user_id, forums.topic, forums.create_date, forums.is_visible, users.username, forum_favorites.user_id, forums.title, forums.description, forums.most_recent_post_date " +
                                                "order by forum_id;", conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ForumsArray forumsArray = new ForumsArray();
                    
                    forumsArray.ForumID = Convert.ToInt32(reader["forum_id"]);
                    forumsArray.Title = Convert.ToString(reader["title"]);
                    forumsArray.Description = Convert.ToString(reader["description"]);
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
                    forumsArray.Title = Convert.ToString(reader["title"]);
                    forumsArray.Description = Convert.ToString(reader["description"]);
                    forumsArray.IsOwner = Convert.ToBoolean(reader["is_owner"]);
                    forumsArray.MostRecentPostDate = Convert.ToDateTime(reader["most_recent_post_date"]);
                    forumsList.Add(forumsArray);
                }
            }
            ForumListDTO forumListDto = new ForumListDTO(forumsList, tokenUserRole);
            return forumListDto;
        }

        public List<ForumModAndUsername> GetAllForumMods()
        {
            List<ForumModAndUsername> forumModAndUsernameList = new List<ForumModAndUsername>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select forum_id, forum_mods.user_id as user_id, username from Forum_mods " +
                    "join users on Forum_mods.user_id = users.user_id " +
                    "order by forum_id;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    ForumModAndUsername forumModAndUsername = new ForumModAndUsername();
                    forumModAndUsername.username = Convert.ToString(reader["username"]);
                    forumModAndUsername.userId = Convert.ToInt32(reader["user_id"]);
                    forumModAndUsername.forumId = Convert.ToInt32(reader["forum_id"]);
                    forumModAndUsernameList.Add(forumModAndUsername);
                }
            }
            return forumModAndUsernameList;
        }

        public List<ForumFavoriteAndUsername> GetAllForumFavorites()
        {
            List<ForumFavoriteAndUsername> forumFavoriteAndUsernameList = new List<ForumFavoriteAndUsername>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select forum_id, Forum_Favorites.user_id as user_id, username from Forum_Favorites " +
                    "join users on Forum_Favorites.user_id = users.user_id " +
                    "order by forum_id;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ForumFavoriteAndUsername forumFavoriteAndUsername = new ForumFavoriteAndUsername();
                    forumFavoriteAndUsername.username = Convert.ToString(reader["username"]);
                    forumFavoriteAndUsername.userId = Convert.ToInt32(reader["user_id"]);
                    forumFavoriteAndUsername.forumId = Convert.ToInt32(reader["forum_id"]);
                    forumFavoriteAndUsernameList.Add(forumFavoriteAndUsername);
                }
            }
            return forumFavoriteAndUsernameList;
        }
        //change from void to something else? 
        
        public int ToggleForumFavorites(int tokenUserId, int favoriteForumId)
        {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                           IF EXISTS (SELECT 1 FROM Forum_Favorites WHERE user_id = @tokenUserId AND forum_id = @favoriteForumId)
                           DELETE FROM Forum_Favorites WHERE user_id = @tokenUserId AND forum_id = @favoriteForumId
                           ELSE
                           INSERT INTO Forum_Favorites (user_id, forum_id) VALUES (@tokenUserId, @favoriteForumId)", conn);

                    cmd.Parameters.AddWithValue("@tokenUserId", tokenUserId);
                    cmd.Parameters.AddWithValue("@favoriteForumId", favoriteForumId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
        }

        public void PostToForum(ForumPost forumPost)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Forum_Posts (header, post_content, is_visible, forum_id, user_id, image_url) " +
                    "values (@header, @post_content, @is_visible, @forum_id, @user_id, @image_url);", conn);
                cmd.Parameters.AddWithValue("@header", forumPost.Header);
                cmd.Parameters.AddWithValue("@post_content", forumPost.PostContent);
                cmd.Parameters.AddWithValue("@is_visible", forumPost.IsVisable);
                cmd.Parameters.AddWithValue("@forum_id", forumPost.ForumID);
                cmd.Parameters.AddWithValue("@user_id", forumPost.UserID);
                cmd.Parameters.AddWithValue("@image_url", forumPost.ImageURL);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetForumOwnerUserID(int forumID)
        {
            int forumOwnerID = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select USER_ID from Forums where forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    forumOwnerID = (Convert.ToInt32(reader["USER_ID"]));
                }
            }
            return forumOwnerID;
        }

        public void DeletePost(int forumID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Forums " +
                    "set is_visible = 0 " +
                    "where forum_id = @forum_id", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.ExecuteNonQuery();
            }
        }

        public List<TopTenPopularPostsArray> GetTopTenPopularPost() 
        {
            List<TopTenPopularPostsArray> topTenPopularPostsList = new List<TopTenPopularPostsArray>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Top 10 ((select count(*) from Post_Upvotes_Downvotes a WHERE a.post_id = Forum_Posts.post_id AND a.is_upvoted = 1 and a.create_date > DATEADD(day, -1, GETDATE())) - " +
                    "(select count(*) from Post_Upvotes_Downvotes b WHERE b.post_id = Forum_Posts.post_id AND b.is_downvoted = 1 and b.create_date > DATEADD(day, -1, GETDATE()))) as UpvotesMinusDownVotesLast24Hours, " +
                    "(select count(*) from Post_Upvotes_Downvotes c WHERE c.post_id = Forum_Posts.post_id AND c.is_upvoted = 1 and c.create_date > DATEADD(day, -1, GETDATE())) as Upvotes24Hours, " +
                    "(select count(*) from Post_Upvotes_Downvotes d WHERE d.post_id = Forum_Posts.post_id AND d.is_downvoted = 1 and d.create_date > DATEADD(day, -1, GETDATE())) as Downvotes24Hours, " +
                    "(select count(*) from Post_Upvotes_Downvotes c WHERE c.post_id = Forum_Posts.post_id AND c.is_upvoted = 1 ) as UpvotesTotal, " +
                    "(select count(*) from Post_Upvotes_Downvotes d WHERE d.post_id = Forum_Posts.post_id AND d.is_downvoted = 1) as DownvotesTotal, " +
                    "Forum_Posts.post_id as post_id, username, post_content as content, header, image_url, Forum_Posts.create_date as create_date, Forum_Posts.user_id as user_id, Forum_Posts.forum_id as forum_id " +
                    "from Forum_Posts " +
                    "join Post_Upvotes_Downvotes on Forum_Posts.post_id = Post_Upvotes_Downvotes.post_id " +
                    "join Forums on Forums.forum_id = Forum_Posts.forum_id " +
                    "join Users on Forum_Posts.user_id = Users.user_id " +
                    "where forums.is_visible = 1 and Forum_Posts.is_visible = 1 " +
                    "group by Forum_Posts.post_id, username, post_content, header, image_url, Forum_Posts.create_date, Forum_Posts.user_id, Forum_Posts.forum_id " +
                    "order by UpvotesMinusDownVotesLast24Hours DESC", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TopTenPopularPostsArray topTenPopularPost = new TopTenPopularPostsArray();

                    topTenPopularPost.postId = Convert.ToInt32(reader["post_id"]);
                    topTenPopularPost.username = Convert.ToString(reader["username"]);
                    topTenPopularPost.content = Convert.ToString(reader["content"]);
                    topTenPopularPost.title = Convert.ToString(reader["header"]);
                    topTenPopularPost.image = Convert.ToString(reader["image_url"]);
                    topTenPopularPost.createDate = Convert.ToDateTime(reader["create_date"]);
                    topTenPopularPost.userId = Convert.ToInt32(reader["user_id"]);
                    topTenPopularPost.forumId = Convert.ToInt32(reader["forum_id"]);
                    topTenPopularPost.upvotesLast24Hours = Convert.ToInt32(reader["Upvotes24Hours"]);
                    topTenPopularPost.downvotesLast24Hours = Convert.ToInt32(reader["Downvotes24Hours"]);
                    topTenPopularPost.upVotes = Convert.ToInt32(reader["UpvotesTotal"]);
                    topTenPopularPost.downVotes = Convert.ToInt32(reader["DownvotesTotal"]);
                    
                    topTenPopularPostsList.Add(topTenPopularPost);
                }
            }
                return topTenPopularPostsList;
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
    }
}

        
    