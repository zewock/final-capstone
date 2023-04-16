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
        private readonly IPostDao postDao;
   
       

        public PostController(IPostDao _postDao)
        {   
            postDao = _postDao;
           
        }

        [HttpGet("/ForumPosts/{forumId}")]

       
        public ActionResult<List<ForumPostWithVotesAndUserName>> GetPostsByForumId(int forumId)
        {
                try
                {
                    var posts = postDao.GetPostsByForumId(forumId);
                    return Ok(posts);
                }
                catch (Exception)
                {
                    
                    return StatusCode(500, new { message = "An error occurred while fetching the posts." });
                }
        }

        [HttpGet("/Posts/{keyword}")]

        public ActionResult<List<ForumPostWithVotesAndUserName>> SearchPosts(string keyword)
        {
            try
            {
                var posts = postDao.SearchPostsForKeyword(keyword);
                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured while fetching the posts." });
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