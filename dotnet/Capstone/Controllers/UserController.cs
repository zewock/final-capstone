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
    public class UserController
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao _userDao)
        {
            userDao = _userDao;
        }
    }
}