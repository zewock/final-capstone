using Capstone.Models.DatabaseModles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public class PostDao
    {
        private readonly string connectionString;
        public PostDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
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
    }
}
