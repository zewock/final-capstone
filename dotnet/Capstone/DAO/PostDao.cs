using Capstone.DAO;
using Capstone.Models.IntermediaryModles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        }
    }
}






        