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
    [Route("")]
    [ApiController]
    public class PrivateMessageController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IForumDao forumDao;
        private readonly IPrivateMessageDao privateMessageDao;

        public PrivateMessageController(IPrivateMessageDao _privateMessageDao, IUserDao _userDao)
        {
            privateMessageDao = _privateMessageDao;
            userDao = _userDao;
        }

        [HttpGet("/PrivateMessages")]
        public ActionResult<PrivateMessagesDTO> GetPrivateMessages()
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
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

            PrivateMessagesDTO privateMessagesDTO = privateMessageDao.GetPrivateMessages(tokenUserId);
            if (privateMessagesDTO.PrivateMessagesArray.Length >= 1)
            {
                privateMessagesDTO.IsUserAdmin = privateMessagesDTO.PrivateMessagesArray[0].IsUserAdmin;
                if (privateMessagesDTO.PrivateMessagesArray[0].IsUserSender)
                {
                    privateMessagesDTO.UserRole = privateMessagesDTO.PrivateMessagesArray[0].FromUserRole;
                }
                else
                {
                    privateMessagesDTO.UserRole = privateMessagesDTO.PrivateMessagesArray[0].ToUserRole;
                }
            }
            else
            {
                privateMessagesDTO.UserRole = privateMessageDao.GetUserRoleFromID(tokenUserId);
                if (privateMessagesDTO.UserRole == "admin")
                {
                    privateMessagesDTO.IsUserAdmin = true;
                }
                else
                {
                    privateMessagesDTO.IsUserAdmin = false;
                }
            }

            return privateMessagesDTO;

            return StatusCode(200, "blah blah");

        }

        [HttpPost("/CreatePrivateMessage")]
        public ActionResult CreatePrivateMessages(CreatePrivateMessageDTO createPrivateMessageDTO)
        {
            int tokenUserId;
            try
            {
                tokenUserId = userDao.GetUser(User.Identity.Name).UserId;
            }
            catch (Exception)
            {
                return StatusCode(401, "You need to be logged in to send a private message");
            }

            //Data valid?

            //privateMessageDao.CreatePrivateMessage(createPrivateMessageDTO, tokenUserId);


            return StatusCode(200, "blah blah");
        }
    }
}