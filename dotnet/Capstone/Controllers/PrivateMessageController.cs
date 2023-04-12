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

        public PrivateMessageController(IPrivateMessageDao _privateMessageDao)
        {
            privateMessageDao = _privateMessageDao;
        }

        [HttpGet("/")]
        public ActionResult GetForums()
        {
            
        return StatusCode(401, "You need to be logged in to create a forum");


        }
    }
}