using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TEbucksServer.DAO;
using Capstone.Models;
using TEBucksServer.DAO;
using TEBucksServer.Security;


namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    public class PostController
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;
        private readonly IForumDao forumDao;

        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            userDao = _userDao;
        }

        [HttpPost("/login")]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = BadRequest(new { message = "Username or password is incorrect" });

            // Get the user by username
            User user = userDao.GetUser(userParam.Username);

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.UserId, user.Username/*, user.Role*/);

                // Create a ReturnUser object to return to the client
                ReturnUser retUser = new ReturnUser() { UserId = user.UserId, Username = user.Username, /*Role = user.Role,*/ Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
            }

            return result;
        }
    }
}