﻿using Capstone.DAO;
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
            string query = BuildQuery();
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
        public string BuildQuery()
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

    


     
        public List<ForumPostWithVotesAndUserName> SearchPostsForKeyword(string keyword)
        {
            string query = @"SELECT 
                    forum_posts.post_id, 
                    forum_posts.forum_id, 
                    forum_posts.user_id, 
                    forum_posts.header, 
                    forum_posts.parent_post_id, 
                    forum_posts.post_content, 
                    forum_posts.is_visible, 
                    forum_posts.create_date, 
                    users.username, 
                    SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS upvotes, 
                    SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS downvotes    
                FROM 
                    forum_posts 
                JOIN 
                    users ON users.user_id = forum_posts.user_id 
                LEFT JOIN 
                    Post_Upvotes_Downvotes pud ON forum_posts.post_id = pud.post_id
                WHERE 
                    (forum_posts.header LIKE @keyword OR forum_posts.post_content LIKE @keyword)
                AND forum_posts.parent_post_id IS NULL
                GROUP BY 
                    forum_posts.post_id, 
                    forum_posts.forum_id, 
                    forum_posts.user_id, 
                    forum_posts.header, 
                    forum_posts.parent_post_id, 
                    forum_posts.post_content, 
                    forum_posts.is_visible, 
                    forum_posts.create_date, 
                    users.username
                ORDER BY 
                    forum_posts.create_date";

            List<ForumPostWithVotesAndUserName> postsWithKeyword = new List<ForumPostWithVotesAndUserName>();
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
                            ForumPostWithVotesAndUserName post = ReadPostFromReader(reader);
                            postsWithKeyword.Add(post);
                        }
                    }
                }
            }
            return postsWithKeyword;
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

        public void PostToForum (PostToForumDTO postToForumDTO, int userID) 
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

        public int GetUserIDByPostID(int postID)
        {
            int postUserID = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select user_id from Forum_Posts where post_id = @post_id", conn);
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
                SqlCommand cmd = new SqlCommand("select user_id from Forum_Mods where forum_id = @forum_id", conn);
                cmd.Parameters.AddWithValue("@forum_id", forumID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    modsIDs.Add(Convert.ToInt32(reader["user_id"]));
                }
            }
            return modsIDs;
        }

        public void DeletePost(int postID) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Forum_Posts " +
                    "set is_visible = 0 " +
                    "where post_id = @post_id;", conn);
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






        