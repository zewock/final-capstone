using Capstone.DAO;
using Capstone.Models.IncomingDTOs;
using Microsoft.AspNetCore.Mvc;



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
            
            return StatusCode(201, "Forum post successfully added to database ");
        }
    }
}