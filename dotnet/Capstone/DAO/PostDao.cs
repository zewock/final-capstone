using Capstone.DAO;
using Capstone.Models.DatabaseModles;
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



        public List<ForumPost> GetPostsByForumId(int forumId)
        {


            string query = @"WITH PostHierarchy AS (
                SELECT posts_id, forum_id, parent_post_id, post_content, header, create_date, is_visible, user_id, 1 as depth
                FROM Forum_Posts 
                WHERE parent_post_id IS NULL AND forum_id = @forumId

                UNION ALL

                SELECT p.posts_id, p.forum_id, p.parent_post_id, p.post_content, p.header, p.create_date, p.is_visible, p.user_id, ph.depth + 1 as depth
                FROM Forum_Posts p
                JOIN PostHierarchy ph ON p.parent_post_id = ph.posts_id
                )
                SELECT 
                ph.*, 
                SUM(CASE WHEN pud.is_upvoted = 1 THEN 1 ELSE 0 END) AS total_upvotes,
                SUM(CASE WHEN pud.is_downvoted = 1 THEN 1 ELSE 0 END) AS total_downvotes
                FROM PostHierarchy ph
                LEFT JOIN Post_Upvotes_Downvotes pud ON ph.posts_id = pud.post_id
                GROUP BY 
                    ph.posts_id, ph.forum_id, ph.parent_post_id, ph.post_content, ph.header, ph.create_date, ph.is_visible, ph.user_id, ph.depth
                ORDER BY ph.depth, ph.create_date";


            List<ForumPost> completePostThreads = new List<ForumPost>();
            Dictionary<long, ForumPost> postDict = new Dictionary<long, ForumPost>();

            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@ForumId", forumId);
                conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ForumPost post = new ForumPost();

                        post.postId = Convert.ToInt32(reader["posts_id"]);
                        post.forumId = Convert.ToInt32(reader["forum_id"]);
                        post.parentPostId = reader["parent_post_id"] == DBNull.Value ? (long?)null : (long)reader["parent_post_id"];
                        post.content = Convert.ToString(reader["post_content"]);
                        post.title = Convert.ToString(reader["header"]);
                        post.createDate = Convert.ToDateTime(reader["create_date"]);
                        post.isVisible = Convert.ToBoolean(reader["is_visible"]);
                        post.userId = Convert.ToInt32(reader["user_id"]);
                        post.depth = Convert.ToInt32(reader["depth"]);
                        post.upVotes = Convert.ToInt32(reader["total_upvotes"]);
                        post.downVotes = Convert.ToInt32(reader["total_downvotes"]);


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






        