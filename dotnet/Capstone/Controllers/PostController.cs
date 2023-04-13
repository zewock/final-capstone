using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IPostDao postDao;

        public PostController(IPostDao _postDao)
        {
            postDao = _postDao;
        }

        [HttpPost("/PostToForum")]
        public IActionResult PostToForum(PostToForumDTO PostToForumDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged in to post to a forum");
            }

            ForumPost forumPost = new ForumPost();
            forumPost.isVisible = true;
            forumPost.path = PostToForumDTO.Path;
            forumPost.image = PostToForumDTO.Image;
            forumPost.content = PostToForumDTO.Content;
            forumPost.forumId = PostToForumDTO.ForumID;
            forumPost.postId = tokenUserId;


            return StatusCode(201, "Forum post successfully added to database ");
        }
    }
}