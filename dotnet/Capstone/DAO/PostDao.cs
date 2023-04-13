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
        public PostDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }




        public List<ForumPost> GetPostsByForumId(int forumId)
        {
            
            string query = @"WITH PostHierarchy AS (
             SELECT  posts_id, forum_id, parent_post_id, post_content, header, create_date, is_visible, user_id, 1 as depth
             FROM Forum_Posts 
             WHERE parent_post_id IS NULL AND forum_id = 1

             UNION ALL

             SELECT p.posts_id, p.forum_id, p.parent_post_id, p.post_content, p.header, p.create_date,  p.is_visible, p.user_id, ph.depth + 1 as depth
             FROM Forum_Posts p
             JOIN PostHierarchy ph ON p.parent_post_id = ph.posts_id
             )
             SELECT * FROM PostHierarchy
             ORDER BY depth, create_date";


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
                        post.header = Convert.ToString(reader["header"]);
                        post.createDate = Convert.ToDateTime(reader["create_date"]);
                        post.isVisible = Convert.ToBoolean(reader["is_visible"]);
                        post.userId = Convert.ToInt32(reader["user_id"]);
                        post.depth = Convert.ToInt32(reader["depth"]);


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



            /*
            //GetPostById(int postId) - Retrieves a post by ID.
            public ActionResult<ForumPost> getPostById(int postId)
            {

            }
            //GetPostsByForumId(int forumId) - Retrieves all posts for a specific forum by ID.
            public ActionResult<List<ForumPost>> getPostsByForumId(int forumId)
            {

            }
            //CreatePost(Post post) - Creates a new post.
            public ActionResult<ForumPost> createPost()
            {

            }
            //UpdatePost(Post post) - Updates an existing post.
            public ActionResult<ForumPost> updatePost(int postId)
            {

            }
            //DeletePost(int postId) - Deletes a post by ID.
            public ActionResult deletePost(int postId)
            {

            }
            */
        }
    }
}
