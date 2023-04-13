using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Capstone.Models;
using Capstone.DAO;
using Capstone.Security;
using Capstone.Models.IncomingDTOs;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Net;
using Capstone.Models.OutgoingDTOs;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    { 
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;
        private readonly IForumDao forumDao;

        public ForumController(IForumDao _forumDao, IUserDao _userDoa)
        {
            forumDao = _forumDao;
            userDao = _userDoa;
        }

        [HttpGet("/ForumsList")]

        public ActionResult<ForumListDTO> getForums()
        {   
                int tokenUserId;
            string tokenUserName;
            string tokenUserRole;
            

            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
                tokenUserName = userDao.GetUser(User.Identity.Name).Username;
                tokenUserRole = userDao.GetUser(User.Identity.Name).Role;
            }
            catch (Exception)
            {
                tokenUserId = 0;
                tokenUserName = "";
                tokenUserRole = "";
            }
            try
            {
                ActionResult<ForumListDTO> forumListDTO = forumDao.getAllForums(tokenUserRole, tokenUserName, tokenUserId);
                return Ok(forumListDTO);

            }
            catch
            {
                return StatusCode(401, "You need to be logged in to create a forum");
            }

        }

        [HttpPost("/CreateForum")]
        public ActionResult CreateForum(CreateForumDTO createForumDTO)
        {
            int tokenUserId;
            try 
            { 
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId; 
            }
            catch(Exception)
            {
                return StatusCode(401, "You need to be logged in to create a forum");
            }

            //Check to see of the user exists 
            if (false)
            {
                return StatusCode(401, "This account does not exist");
            }

            //Check to see of the user is banned
            if (false)
            {
                return StatusCode(401, "This account is banned");
            }

            //Data valid?

            Forum forum = new Forum();
            forum.ownerId = tokenUserId;
            forum.title = createForumDTO.Title;
            forum.topic = createForumDTO.Topic;
            forum.isVisible = true;

            forumDao.CreateForum(forum);

            return StatusCode(200, "blah blah");
        }

    }
}
