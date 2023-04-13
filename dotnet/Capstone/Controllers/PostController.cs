using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;
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

        public List<ForumPost> GetPostsByForumId(int id)
        {
            return postDao.GetPostsByForumId(id);
        }
        
        [HttpPost("/PostToForum")]
        public IActionResult PostToForum(PostToForumDTO PostToForumDTO)
        {
            
            return StatusCode(201, "Forum post successfully added to database ");
        }
    }
}