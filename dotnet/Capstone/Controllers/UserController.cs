using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using Capstone.Models.IncomingDTOs;

namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao _userDao)
        {
            userDao = _userDao;
        }

        [HttpPut("/BanUser")]
        public ActionResult BanUser(BanUserDTO banUserDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged to bann a user");
            }
            
            string tokenUserRole = userDao.GetUserRoleById(tokenUserId);
            if (tokenUserRole != "admin")
            {
                return StatusCode(401, "Only admins can bann a user");
            }

            userDao.SetBanTime(banUserDTO);

            if (banUserDTO.DeleteAllTraffic)
            {
                userDao.DeleteUsersContent(banUserDTO);
                return StatusCode(200, "User was successfully banned, all users content was deleted");
            }
            else
            {
                return StatusCode(200, "User was successfully banned");
            }
        }
    }
}