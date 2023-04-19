using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Capstone.Models.IntermediaryModles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Web.Mvc.Html;


namespace Capstone.DAO
{
    public class PostDao : IPostDao
    {
        private readonly string connectionString;
        public PostDao(string ConnectionString)
        {
            connectionString = ConnectionString;
        }


        public List<ForumPostWithVotesAndUserName> GetPostsByForumId(int forumId)
        {
            string query = BuildPostByForumQuery();
            Dictionary<long, ForumPostWithVotesAndUserName> postDict = new Dictionary<long, ForumPostWithVotesAndUserName>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                using (SqlCommand command = new SqlCommand(query, conn))
                {   
                    command.Parameters.AddWithValue("@forumId", forumId);
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ForumPostWithVotesAndUserName post = ReadPostFromReader(reader, true);
                            AddPostToHierarchy(post, postDict);
                            UpvotesDownvotesInLast24H upvotesDownvotes = readUpvotesDownVotesIn24H(reader);
                            postDict[upvotesDownvotes.post_id].upvotesLast24Hours = upvotesDownvotes.upvotesInLast24H;
                            postDict[upvotesDownvotes.post_id].downvotesLast24Hours = upvotesDownvotes.downvotesInLast24H;

                        }
                    }
                }
                return GetCompletePostThreads(postDict);
            }

        }
        public List<ForumPostWithVotesAndUserName> GetPostsByPostId(int postId)
        {
            string query = BuildPostByPostIdQuery();
            Dictionary<long, ForumPostWithVotesAndUserName> postDict = new Dictionary<long, ForumPostWithVotesAndUserName>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@postId", postId);
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ForumPostWithVotesAndUserName post = ReadPostFromReader(reader, true);
                            AddPostToHierarchy(post, postDict);
                            UpvotesDownvotesInLast24H upvotesDownvotes = readUpvotesDownVotesIn24H(reader);
                            postDict[upvotesDownvotes.post_id].upvotesLast24Hours = upvotesDownvotes.upvotesInLast24H;
                            postDict[upvotesDownvotes.post_id].downvotesLast24Hours = upvotesDownvotes.downvotesInLast24H;

                        }
                    }
                }
                return GetCompletePostThreads(postDict);
            }

        }

        
        public string BuildPostByForumQuery()
        {
            string query = @"
                            WITH PostHierarchy AS (
                                SELECT
                                    fp.post_id,
                                    fp.forum_id,
                                    fp.user_id,
                                    fp.header,
                                    fp.parent_post_id,
                                    fp.post_content,
                                    fp.is_visible,
                                    fp.create_date,
                                    u.username,
                                    0 AS depth
                                FROM forum_posts fp
                                JOIN users u ON fp.user_id = u.user_id
                                WHERE fp.parent_post_id IS NULL AND fp.forum_id = @forumId
                                

                                UNION ALL

                                SELECT
                                    fp.post_id,
                                    fp.forum_id,
                                    fp.user_id,
                                    fp.header,
                                    fp.parent_post_id,
                                    fp.post_content,
                                    fp.is_visible,
                                    fp.create_date,
                                    u.username,
                                    ph.depth + 1
                                FROM forum_posts fp
                                JOIN users u ON fp.user_id = u.user_id
                                JOIN PostHierarchy ph ON fp.parent_post_id = ph.post_id
                                WHERE fp.forum_id = @forumId
                           
                            )
                            SELECT
                                ph.post_id,
                                ph.forum_id,
                                ph.user_id,
                                ph.header,
                                ph.parent_post_id,
                                ph.post_content,
                                ph.is_visible,
                                ph.create_date,
                                ph.username,
                                ph.depth,
                                SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes,
                                SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes,
                                SUM(CASE WHEN pud.is_upvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS upvotes_last_24h,
                                SUM(CASE WHEN pud.is_downvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS downvotes_last_24h
                            FROM PostHierarchy ph
                            LEFT JOIN Post_Upvotes_Downvotes pud ON ph.post_id = pud.post_id
                            GROUP BY
                                ph.post_id,
                                ph.forum_id,
                                ph.user_id,
                                ph.header,
                                ph.parent_post_id,
                                ph.post_content,
                                ph.is_visible,
                                ph.create_date,
                                ph.username,
                                ph.depth
                            ORDER BY ph.depth, ph.create_date";
            return query;
        }

        public string BuildPostByPostIdQuery()
        {
            string query = @"WITH PostHierarchy AS (
                                SELECT
                                    fp.post_id,
                                    fp.forum_id,
                                    fp.user_id,
                                    fp.header,
                                    fp.parent_post_id,
                                    fp.post_content,
                                    fp.is_visible,
                                    fp.create_date,
                                    u.username,
                                    0 AS depth
                                FROM forum_posts fp
                                JOIN users u ON fp.user_id = u.user_id
                                WHERE fp.post_id = @postId 
                                

                                UNION ALL

                                SELECT
                                    fp.post_id,
                                    fp.forum_id,
                                    fp.user_id,
                                    fp.header,
                                    fp.parent_post_id,
                                    fp.post_content,
                                    fp.is_visible,
                                    fp.create_date,
                                    u.username,
                                    ph.depth + 1
                                FROM forum_posts fp
                                JOIN users u ON fp.user_id = u.user_id
                                JOIN PostHierarchy ph ON fp.parent_post_id = ph.post_id
                                
                           
                            )
                            SELECT
                                ph.post_id,
                                ph.forum_id,
                                ph.user_id,
                                ph.header,
                                ph.parent_post_id,
                                ph.post_content,
                                ph.is_visible,
                                ph.create_date,
                                ph.username,
                                ph.depth,
                                SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes,
                                SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes,
                                SUM(CASE WHEN pud.is_upvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS upvotes_last_24h,
                                SUM(CASE WHEN pud.is_downvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS downvotes_last_24h
                            FROM PostHierarchy ph
                            LEFT JOIN Post_Upvotes_Downvotes pud ON ph.post_id = pud.post_id
                            GROUP BY
                                ph.post_id,
                                ph.forum_id,
                                ph.user_id,
                                ph.header,
                                ph.parent_post_id,
                                ph.post_content,
                                ph.is_visible,
                                ph.create_date,
                                ph.username,
                                ph.depth
                            ORDER BY ph.depth, ph.create_date";
            return query;
        }


     
        public List<ForumPostWithVotesAndUserName> SearchPostsForKeyword(string keyword)
        {
            string query = @"WITH PostHierarchy AS (
                    SELECT
                        fp.post_id,
                        fp.forum_id,
                        fp.user_id,
                        fp.header,
                        fp.parent_post_id,
                        fp.post_content,
                        fp.is_visible,
                        fp.create_date,
                        u.username,
                        0 AS depth
                    FROM forum_posts fp
                    JOIN users u ON fp.user_id = u.user_id
                    WHERE (fp.header LIKE @keyword OR fp.post_content LIKE @keyword) AND fp.parent_post_id IS NULL

                    UNION ALL

                    SELECT
                        fp.post_id,
                        fp.forum_id,
                        fp.user_id,
                        fp.header,
                        fp.parent_post_id,
                        fp.post_content,
                        fp.is_visible,
                        fp.create_date,
                        u.username,
                        ph.depth + 1
                    FROM forum_posts fp
                    JOIN users u ON fp.user_id = u.user_id
                    JOIN PostHierarchy ph ON fp.parent_post_id = ph.post_id
                )
                SELECT
                    ph.post_id,
                    ph.forum_id,
                    ph.user_id,
                    ph.header,
                    ph.parent_post_id,
                    ph.post_content,
                    ph.is_visible,
                    ph.create_date,
                    ph.username,
                    ph.depth,
                    SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes,
                    SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes
                FROM PostHierarchy ph
                LEFT JOIN Post_Upvotes_Downvotes pud ON ph.post_id = pud.post_id
                GROUP BY
                    ph.post_id,
                    ph.forum_id,
                    ph.user_id,
                    ph.header,
                    ph.parent_post_id,
                    ph.post_content,
                    ph.is_visible,
                    ph.create_date,
                    ph.username,
                    ph.depth
                ORDER BY ph.depth, ph.create_date";

            List<ForumPostWithVotesAndUserName> postsWithKeyword = new List<ForumPostWithVotesAndUserName>();
            Dictionary<long, ForumPostWithVotesAndUserName> postDict = new Dictionary<long, ForumPostWithVotesAndUserName>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@keyword", $"%{keyword}%");
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {   
                        while (reader.Read())
                        {
                            ForumPostWithVotesAndUserName post = ReadPostFromReader(reader, true);
                            postsWithKeyword.Add(post);
                            AddPostToHierarchy(post, postDict);
                            UpvotesDownvotesInLast24H upvotesDownvotes = readUpvotesDownVotesIn24H(reader);
                            postDict[upvotesDownvotes.post_id].upvotesLast24Hours = upvotesDownvotes.upvotesInLast24H;
                            postDict[upvotesDownvotes.post_id].downvotesLast24Hours = upvotesDownvotes.downvotesInLast24H;
                        }
                    }
                }
            }
            return GetCompletePostThreads(postDict);
        }

        public List<ForumPostWithVotesAndUserName> GetCompletePostThreadById(int postId)
        {
            string threadQuery = @"
                    WITH PostHierarchy AS (
                        SELECT
                            fp.post_id,
                            fp.forum_id,
                            fp.user_id,
                            fp.header,
                            fp.parent_post_id,
                            fp.post_content,
                            fp.is_visible,
                            fp.create_date,
                            u.username,
                            0 AS depth
                        FROM forum_posts fp
                        JOIN users u ON fp.user_id = u.user_id
                        WHERE fp.parent_post_id IS NULL

                        UNION ALL

                        SELECT
                            fp.post_id,
                            fp.forum_id,
                            fp.user_id,
                            fp.header,
                            fp.parent_post_id,
                            fp.post_content,
                            fp.is_visible,
                            fp.create_date,
                            u.username,
                            ph.depth + 1
                        FROM forum_posts fp
                        JOIN users u ON fp.user_id = u.user_id
                        JOIN PostHierarchy ph ON fp.parent_post_id = ph.post_id
                    ),
                    RootPost AS (
                        SELECT 
                            COALESCE(MAX(ph.parent_post_id), @postId) AS root_post_id
                        FROM PostHierarchy ph
                        WHERE ph.post_id = @postId OR ph.parent_post_id = @postId
                    )
                    SELECT
                        ph.post_id,
                        ph.forum_id,
                        ph.user_id,
                        ph.header,
                        ph.parent_post_id,
                        ph.post_content,
                        ph.is_visible,
                        ph.create_date,
                        ph.username,
                        ph.depth,
                        SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes,
                        SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes,
                        SUM(CASE WHEN pud.is_upvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS upvotes_last_24h,
                        SUM(CASE WHEN pud.is_downvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS downvotes_last_24h
                    FROM PostHierarchy ph
                    LEFT JOIN Post_Upvotes_Downvotes pud ON ph.post_id = pud.post_id
                    INNER JOIN RootPost rp ON ph.post_id = rp.root_post_id OR ph.parent_post_id = rp.root_post_id
                    GROUP BY
                        ph.post_id,
                        ph.forum_id,
                        ph.user_id,
                        ph.header,
                        ph.parent_post_id,
                        ph.post_content,
                        ph.is_visible,
                        ph.create_date,
                        ph.username,
                        ph.depth
                    ORDER BY ph.depth, ph.create_date";

            Dictionary<long, ForumPostWithVotesAndUserName> postDict = new Dictionary<long, ForumPostWithVotesAndUserName>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(threadQuery, conn))
                {
                    command.Parameters.AddWithValue("@postId", postId);
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ForumPostWithVotesAndUserName post = ReadPostFromReader(reader, true);
                            AddPostToHierarchy(post, postDict);
                            UpvotesDownvotesInLast24H upvotesDownvotes = readUpvotesDownVotesIn24H(reader);
                            postDict[upvotesDownvotes.post_id].upvotesLast24Hours = upvotesDownvotes.upvotesInLast24H;
                            postDict[upvotesDownvotes.post_id].downvotesLast24Hours = upvotesDownvotes.downvotesInLast24H;
                        }
                    }
                }
            }
            return GetCompletePostThreads(postDict);
        }

        public void PostToForum(PostToForumDTO postToForumDTO, int userID)
        {
            if (postToForumDTO.ParentPostID != null)
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Forum_Posts " +
                        "(header, parent_post_id, post_content, is_visible, forum_id, user_id, image_url) " +
                        "values (@header, @parent_post_id, @post_content, @is_visible, @forum_id, @user_id, @image_url);", conn);
                    cmd.Parameters.AddWithValue("@header", postToForumDTO.Header);
                    cmd.Parameters.AddWithValue("@parent_post_id", postToForumDTO.ParentPostID);
                    cmd.Parameters.AddWithValue("@post_content", postToForumDTO.Content);
                    cmd.Parameters.AddWithValue("@is_visible", true);
                    cmd.Parameters.AddWithValue("@forum_id", postToForumDTO.ForumID);
                    cmd.Parameters.AddWithValue("@user_id", userID);
                    cmd.Parameters.AddWithValue("@image_url", postToForumDTO.Image);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Forum_Posts " +
                        "(header, post_content, is_visible, forum_id, user_id, image_url) " +
                        "values (@header, @post_content, @is_visible, @forum_id, @user_id, @image_url);", conn);
                    cmd.Parameters.AddWithValue("@header", postToForumDTO.Header);
                    cmd.Parameters.AddWithValue("@post_content", postToForumDTO.Content);
                    cmd.Parameters.AddWithValue("@is_visible", true);
                    cmd.Parameters.AddWithValue("@forum_id", postToForumDTO.ForumID);
                    cmd.Parameters.AddWithValue("@user_id", userID);
                    cmd.Parameters.AddWithValue("@image_url", postToForumDTO.Image);
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Forums " +
                    "set most_recent_post_date = @most_recent_post_date " +
                    "where forum_id = @forum_id", conn);
                cmd.Parameters.AddWithValue("@forum_id", postToForumDTO.ForumID);
                cmd.Parameters.AddWithValue("@most_recent_post_date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

        }

        public int GetUserIDByPostID(int postID)
        {
            int postUserID = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select user_id from Forum_Posts where post_id = @post_id;", conn);
                cmd.Parameters.AddWithValue("@post_id", postID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    postUserID = Convert.ToInt32(reader["user_id"]);
                }
            }
            return postUserID;
        }

        public List<int> GetModsIDsByForumID(int forumID)
        {
            List<int> modsIDs = new List<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select user_id from Forum_Mods where forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    modsIDs.Add(Convert.ToInt32(reader["user_id"]));
                }
            }
            return modsIDs;
        }

        public int IsUserOwnerOfForum(int userID, int forumID)
        {
            int isOwnerOfForum = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Count(*) as isUserOwnerOfForum from Forums " +
                    "where user_id = @user_id and forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@user_id", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isOwnerOfForum = (Convert.ToInt32(reader["isUserOwnerOfForum"]));
                }
            }
            return isOwnerOfForum;
        }

        public int IsUserModOfForum(string username, int forumID)
        {
            int isUserModOfForum = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select count(*) as isUserModOfForum from Forum_Mods " +
                    "join users on Forum_Mods.user_id = users.user_id " +
                    "where username = @username and forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isUserModOfForum = (Convert.ToInt32(reader["isUserModOfForum"]));
                }
            }
            return isUserModOfForum;
        }

        public void AddMod(int userID, int forumID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Forum_Mods (forum_id, user_id) " +
                    "values (@forum_id, @user_id);", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@user_id", userID);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveMod(int userID, int forumID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from Forum_Mods where user_id = @userID and forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
            }
        }

        public int IsForumFavorited (int userID, int forumID)
        {
            int isForumFavorited = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Count(*) as isForumFavorited from Forum_Favorites " +
                    "where user_id = @user_id and forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@user_id", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isForumFavorited = (Convert.ToInt32(reader["isForumFavorited"]));
                }
            }
            return isForumFavorited;
        }

        public void RemoveFavorite(int userID, int forumID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from Forum_Favorites where user_id = @userID and forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddFavoriteForum(int userID, int forumID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Forum_Favorites (forum_id, user_id) " +
                    "values (@forum_id, @user_id);", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@user_id", userID);
                cmd.ExecuteNonQuery();
            }
        }

        public bool DoseForumExist (int forumID)
        {
            int doseForumExist = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Count(*) as doseForumExist from Forums " +
                    "where forum_id = @forum_id", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    doseForumExist = (Convert.ToInt32(reader["doseForumExist"]));
                }
            }
            return doseForumExist >= 1;
        }

        public IsUpvotedDownVoted GetPostsUpvotesDownvotes (int userID, int postID, IsUpvotedDownVoted isUpvotedDownVoted)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select post_id, is_upvoted, is_downvoted from Post_Upvotes_Downvotes " +
                    "where post_id = @post_id and user_id = @user_id;", conn);
                cmd.Parameters.AddWithValue("@post_id", postID);
                cmd.Parameters.AddWithValue("@user_id", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isUpvotedDownVoted.postID = Convert.ToInt32(reader["post_id"]);
                    isUpvotedDownVoted.isUpvoted = Convert.ToBoolean(reader["is_upvoted"]);
                    isUpvotedDownVoted.isDownvoted = Convert.ToBoolean(reader["is_downvoted"]);
                }
            }
            return isUpvotedDownVoted;
        }


        public bool DosePostUpvoteDownExist (int postID, int forumID)

        {
            int dosePostExist = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Count(*) as dosePostExist from Post_Upvotes_Downvotes " +
                    "where post_id = @post_id and forum_id = @forum_id;", conn);
                cmd.Parameters.AddWithValue("@post_id", postID);
                cmd.Parameters.AddWithValue("@forum_id", forumID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dosePostExist = (Convert.ToInt32(reader["dosePostExist"]));
                }
            }
            return dosePostExist >= 1;
        }

        public bool DosePostExist(int postID, int forumID)
        {
            int dosePostExist = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Count(*) as dosePostExist from Forum_Posts " +
                    "where post_id = @post_id and forum_id = @forum_id and is_visible = 1;", conn);
                cmd.Parameters.AddWithValue("@post_id", postID);
                cmd.Parameters.AddWithValue("@forum_id", forumID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dosePostExist = (Convert.ToInt32(reader["dosePostExist"]));
                }
            }
            return dosePostExist >= 1;
        }

        public void CreateUpvoteDownvote (PostsUpvotesDownvotes postsUpvotesDownvotes) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Post_Upvotes_Downvotes (forum_id, post_id, is_upvoted, is_downvoted, user_id) " +
                    "values (@forum_id, @post_id, @is_upvoted, @is_downvoted, @user_id);", conn);
                cmd.Parameters.AddWithValue("@forum_id", postsUpvotesDownvotes.forumId);
                cmd.Parameters.AddWithValue("@user_id", postsUpvotesDownvotes.userId);
                cmd.Parameters.AddWithValue("@post_id", postsUpvotesDownvotes.postId);
                cmd.Parameters.AddWithValue("@is_upvoted", postsUpvotesDownvotes.isUpVoted);
                cmd.Parameters.AddWithValue("@is_downvoted", postsUpvotesDownvotes.isDownVoted);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUpvoteDownvote (int userID, int postID) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from Post_Upvotes_Downvotes where user_id = @user_id and post_id = @post_id;", conn);
                cmd.Parameters.AddWithValue("@post_id", postID);
                cmd.Parameters.AddWithValue("@user_id", userID);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUpvoteDownvote (PostsUpvotesDownvotes postsUpvotesDownvotes)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Post_Upvotes_Downvotes " +
                    "set is_upvoted = @is_upvoted, is_downvoted = @is_downvoted, create_date = @create_date " +
                    "where user_id = @user_id and post_id = @post_id", conn);
                cmd.Parameters.AddWithValue("@is_upvoted", postsUpvotesDownvotes.isUpVoted);
                cmd.Parameters.AddWithValue("@is_downvoted", postsUpvotesDownvotes.isDownVoted);
                cmd.Parameters.AddWithValue("@create_date", postsUpvotesDownvotes.createDate);
                cmd.Parameters.AddWithValue("@user_id", postsUpvotesDownvotes.userId);
                cmd.Parameters.AddWithValue("@post_id", postsUpvotesDownvotes.postId);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetForumOwnerUserID (int forumID) 
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

        public List<IDsAndParentIDsPostsInForum> GetIDsAndParentIDsInForm (int forumID, int postID)
        {
            List<IDsAndParentIDsPostsInForum> idsAndParentIDsPostsInForums = new List<IDsAndParentIDsPostsInForum>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select post_id, " +
                    "SUM(CASE WHEN parent_post_id is NULL THEN 0 ELSE parent_post_id END) AS parent_post_id " +
                    "from Forum_Posts " +
                    "where forum_id = @forum_id and post_id > @post_id " +
                    "group by post_id " +
                    "order by post_id", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                cmd.Parameters.AddWithValue("@post_id", postID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IDsAndParentIDsPostsInForum iDsAndParentIDsPostsInForum = new IDsAndParentIDsPostsInForum();
                    iDsAndParentIDsPostsInForum.PostID = Convert.ToInt32(reader["post_id"]);
                    iDsAndParentIDsPostsInForum.ParentPostID = Convert.ToInt32(reader["parent_post_id"]);
                    idsAndParentIDsPostsInForums.Add(iDsAndParentIDsPostsInForum);
                }
            }
            return idsAndParentIDsPostsInForums;
        }

        public void DeletePost(int postID) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Forum_Posts " +
                    "set is_visible = 0 " +
                    "where post_id = @post_id or parent_post_id = @post_id;", conn);
                cmd.Parameters.AddWithValue("@post_id", postID);
                cmd.ExecuteNonQuery();
            }
        }


        private void AddPostToHierarchy(ForumPostWithVotesAndUserName post, Dictionary<long, ForumPostWithVotesAndUserName> postDict)
        {
            postDict[post.postId] = post;

            if (post.parentPostId == null)
            {
                postDict[post.postId].replies = new List<ForumPostWithVotesAndUserName>();
            }
            else
            {
                postDict[post.parentPostId.Value].replies.Add(post);
            }
        }

        private List<ForumPostWithVotesAndUserName> GetCompletePostThreads(Dictionary<long, ForumPostWithVotesAndUserName> postDict)
        {
            List<ForumPostWithVotesAndUserName> completePostThreads = new List<ForumPostWithVotesAndUserName>();

            foreach (var post in postDict.Values)
            {
                if (post.parentPostId == null)
                {
                    completePostThreads.Add(post);
                }
            }
            return completePostThreads;
        }
        private ForumPostWithVotesAndUserName ReadPostFromReader(SqlDataReader reader, bool readDepth = false)
        {
            ForumPostWithVotesAndUserName post = new ForumPostWithVotesAndUserName();

            post.postId = Convert.ToInt32(reader["post_id"]);
            post.forumId = Convert.ToInt32(reader["forum_id"]);
            post.username = Convert.ToString(reader["username"]);
            post.parentPostId = reader["parent_post_id"] == DBNull.Value ? (long?)null : (long)reader["parent_post_id"];
            post.content = Convert.ToString(reader["post_content"]);
            post.title = Convert.ToString(reader["header"]);
            post.createDate = Convert.ToDateTime(reader["create_date"]);
            post.isVisible = Convert.ToBoolean(reader["is_visible"]);
            post.userId = Convert.ToInt32(reader["user_id"]);
            post.upVotes = Convert.ToInt32(reader["upvotes"]);
            post.downVotes = Convert.ToInt32(reader["downvotes"]);

            if (readDepth)
            {
                post.depth = Convert.ToInt32(reader["depth"]);
            }



            return post;
        }


        private UpvotesDownvotesInLast24H readUpvotesDownVotesIn24H(SqlDataReader reader) 
        {
            UpvotesDownvotesInLast24H upvotesDownvotesIn24H = new UpvotesDownvotesInLast24H();
            upvotesDownvotesIn24H.post_id = Convert.ToInt32(reader["post_id"]);
            upvotesDownvotesIn24H.forum_id = Convert.ToInt32(reader["forum_id"]);
            upvotesDownvotesIn24H.upvotesInLast24H = Convert.ToInt32(reader["upvotes"]);
            upvotesDownvotesIn24H.downvotesInLast24H = Convert.ToInt32(reader["downvotes"]);
            return upvotesDownvotesIn24H; 
        }



           /* string query = @"
                    WITH PostHierarchy AS (
                        SELECT
                            fp.post_id,
                            fp.forum_id,
                            fp.user_id,
                            fp.header,
                            fp.parent_post_id,
                            fp.post_content,
                            fp.is_visible,
                            fp.create_date,
                            u.username,
                            0 AS depth
                        FROM forum_posts fp
                        JOIN users u ON fp.user_id = u.user_id
                        WHERE fp.parent_post_id IS NULL

                        UNION ALL

                        SELECT
                            fp.post_id,
                            fp.forum_id,
                            fp.user_id,
                            fp.header,
                            fp.parent_post_id,
                            fp.post_content,
                            fp.is_visible,
                            fp.create_date,
                            u.username,
                            ph.depth + 1
                        FROM forum_posts fp
                        JOIN users u ON fp.user_id = u.user_id
                        JOIN PostHierarchy ph ON fp.parent_post_id = ph.post_id
                    ),
                    PostsContainingKeyword AS (
                        SELECT post_id
                        FROM PostHierarchy
                        WHERE header LIKE @keyword 
                    ),
                    RootPosts AS (
                        SELECT DISTINCT
                            COALESCE(
                                (SELECT top 1 parent_post_id FROM PostHierarchy WHERE post_id = pck.post_id AND parent_post_id IS NOT NULL ORDER BY depth DESC),
                                pck.post_id
                            ) AS root_post_id
                        FROM PostsContainingKeyword pck
                    )
                    SELECT
                        ph.post_id,
                        ph.forum_id,
                        ph.user_id,
                        ph.header,
                        ph.parent_post_id,
                        ph.post_content,
                        ph.is_visible,
                        ph.create_date,
                        ph.username,
                        ph.depth,
                        SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes,
                        SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes,
                        SUM(CASE WHEN pud.is_upvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS upvotes_last_24h,
                        SUM(CASE WHEN pud.is_downvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS downvotes_last_24h
                    FROM PostHierarchy ph
                    LEFT JOIN Post_Upvotes_Downvotes pud ON ph.post_id = pud.post_id
                    INNER JOIN RootPosts rp ON ph.post_id = rp.root_post_id OR ph.parent_post_id = rp.root_post_id
                    GROUP BY
                        ph.post_id,
                        ph.forum_id,
                        ph.user_id,
                        ph.header,
                        ph.parent_post_id,
                        ph.post_content,
                        ph.is_visible,
                        ph.create_date,
                        ph.username,
                        ph.depth
                    ORDER BY ph.depth, ph.create_date";*/

           
        



        /* public List<ForumPostWithVotesAndUserName> GetPostsByForumId(int forumId)
         {


             string query = @"WITH PostHierarchy AS (

                 SELECT fp.posts_id, fp.forum_id, fp.user_id, fp.header, fp.parent_post_id, fp.post_content, fp.is_visible, fp.create_date, u.username, 0 AS depth 
                 FROM forum_posts fp
                 JOIN users u ON fp.user_id = u.user_id
                 WHERE
                 fp.parent_post_id IS NULL

                 UNION ALL

                SELECT fp.posts_id, fp.forum_id, fp.user_id, fp.header, fp.parent_post_id, fp.post_content, fp.is_visible, fp.create_date, u.username, ph.depth + 1
                FROM forum_posts fp
                JOIN users u ON fp.user_id = u.user_id
                JOIN PostHierarchy ph ON fp.parent_post_id = ph.posts_id
                )
                SELECT ph.posts_id, ph.forum_id, ph.user_id, ph.header, ph.parent_post_id, ph.post_content, ph.is_visible, ph.create_date, ph.username, ph.depth,
                SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes,
                SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes,
                SUM(CASE WHEN pud.is_upvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS upvotes_last_24h,
                SUM(CASE WHEN pud.is_downvoted = 1 AND pud.create_date > DATEADD(day, -1, GETDATE()) THEN 1 ELSE 0 END) AS downvotes_last_24h
                FROM
                PostHierarchy ph
                LEFT JOIN Post_Upvotes_Downvotes pud ON ph.posts_id = pud.post_id
                GROUP BY ph.posts_id, ph.forum_id, ph.user_id, ph.header, ph.parent_post_id, ph.post_content, ph.is_visible, ph.create_date, ph.username, ph.depth
                ORDER BY ph.depth, ph.create_date";

                List<ForumPostWithVotesAndUserName> completePostThreads = new List<ForumPostWithVotesAndUserName>();
                Dictionary<long, ForumPostWithVotesAndUserName> postDict = new Dictionary<long, ForumPostWithVotesAndUserName>();

                SqlConnection conn = new SqlConnection(connectionString);

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                 command.Parameters.AddWithValue("@ForumId", forumId);
                 conn.Open();

                 using (SqlDataReader reader = command.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         ForumPostWithVotesAndUserName post = new ForumPostWithVotesAndUserName();

                         post.postId = Convert.ToInt32(reader["posts_id"]);
                         post.forumId = Convert.ToInt32(reader["forum_id"]);
                         post.username = Convert.ToString(reader["username"]);
                         post.parentPostId = reader["parent_post_id"] == DBNull.Value ? (long?)null : (long)reader["parent_post_id"];
                         post.content = Convert.ToString(reader["post_content"]);
                         post.title = Convert.ToString(reader["header"]);
                         post.createDate = Convert.ToDateTime(reader["create_date"]);
                         post.isVisible = Convert.ToBoolean(reader["is_visible"]);
                         post.userId = Convert.ToInt32(reader["user_id"]);
                         post.depth = Convert.ToInt32(reader["depth"]);
                         post.upVotes = Convert.ToInt32(reader["upvotes"]);
                         post.downVotes = Convert.ToInt32(reader["downvotes"]);
                         post.upvotesLast24Hours = Convert.ToInt32(reader["upvotes_last_24h"]);
                         post.downvotesLast24Hours = Convert.ToInt32(reader["downvotes_last_24h"]);


                         postDict[post.postId] = post;

                         if (post.parentPostId == null)
                         {
                             completePostThreads.Add(post);
                         }
                         else
                         {
                             postDict[post.parentPostId.Value].replies.Add(post);
                         }

                     };
                 };


                 return completePostThreads;
             };
         }*/
      
    }
}






        