using Capstone.DAO;
using Capstone.Models.IncomingDTOs;
using Capstone.Models.IntermediaryModles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        [HttpGet("/ForumPosts/{id}")]

        public ActionResult<List<ForumPostWithVotesAndUserName>> GetPostsByForumId(int id)
        {
            try
            {
                List<ForumPostWithVotesAndUserName> posts =  postDao.GetPostsByForumId(id);
                return posts;
            }
            catch(Exception)
            {
                return StatusCode(404, "not found");
            }
        }
        
       /* [HttpPost("/PostToForum")]

       
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
            forumPost.image = PostToForumDTO.Image;
            forumPost.content = PostToForumDTO.Content;
            forumPost.forumId = PostToForumDTO.ForumID;
            forumPost.postId = tokenUserId;


            return StatusCode(201, "Forum post successfully added to database ");
        }*/
    }
}